using System;
using System.Reflection;

namespace HandlerInvoker.Core.Helpers
{
    internal static class ParameterHelper
    {
        public static object[] GetMethodParametersDefaultValues(MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException();
            }

            ParameterInfo[] methodParameters = methodInfo.GetParameters();
            var methodParametersValues = new object[methodParameters.Length];

            for (int i = 0; i < methodParametersValues.Length; i++)
            {
                methodParametersValues[i] = GetParameterDefaultValue(methodParameters[i]);
            }

            return methodParametersValues;
        }

        public static object GetParameterDefaultValue(ParameterInfo parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException();
            }

            if (parameter.HasDefaultValue)
            {
                return parameter.DefaultValue;
            }

            if (parameter.ParameterType.IsValueType)
            {
                return Activator.CreateInstance(parameter.ParameterType);
            }

            return null;
        }
    }
}
