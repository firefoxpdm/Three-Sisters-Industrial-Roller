using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace ThreeSistersIndustrialRoller
{
    class Controller : Mod
    {
        private static Harmony harmony;
        public Controller(ModContentPack content) : base(content)
        {
            harmony = new Harmony("com.firefox.threesistersindustrialroller");
            harmony.Patch(
                original: AccessTools.Method("RimWorldIndustrialRollers.MovingRailGreenPuller:DoTickerWork"),
                transpiler: new HarmonyMethod(
                    methodType: typeof(Controller),
                    methodName: nameof(patchGreenPullerWork))
                );
        }

        static IEnumerable<CodeInstruction> patchGreenPullerWork(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode.Name.Equals("ldftn"))
                {
                    harmony.Patch(
                        original: codes[i].operand as MethodInfo,
                        transpiler: new HarmonyMethod(
                            methodType: typeof(Controller),
                            methodName: nameof(patchGreenPullerWorkInternal)
                        ));
                }
            }
            return codes.AsEnumerable();
        }

        static IEnumerable<CodeInstruction> patchGreenPullerWorkInternal(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode.Name.Equals("callvirt"))
                {
                    codes[i] = new CodeInstruction(OpCodes.Call, typeof(ThreeSistersResolver).GetMethod("ItemMatchesGrowthZonePlantProduce"));
                }
                
            }
            return codes.AsEnumerable();
        }

   
    }
}
