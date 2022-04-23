using System.Collections.Generic;
using System.Text;
using MelonLoader;
using OscCore;
using VRC.SDK3.Avatars.ScriptableObjects;

namespace TurbOSC
{
    public static class IKSync
    {
        public static void ReadValues(OscMessageValues values)
        {
            List<string> recvValues = new List<string>();

            for (int i = values.Offsets[0]; i < values.m_SharedBuffer.Length;)
            {
                List<byte> currParam = new List<byte>();
                while (values.m_SharedBuffer[i] != 0)
                {
                    currParam.Add(values.m_SharedBuffer[i]);
                    i++;
                }

                // We can assume that the next byte is a null, because we know that the last byte of the message is a null
                i++;

                if (currParam.Count == 0) continue;
                
                recvValues.Add(Encoding.UTF8.GetString(currParam.ToArray()));
            }

            foreach (var paramName in recvValues)
            {
                var param = ParamLib.ParamLib.FindParam(paramName, VRCExpressionParameters.ValueType.Float);
                if (param[0] == null) continue;
                ParamLib.ParamLib.PrioritizeParameter(param[0]);
            }
        }
    }
}