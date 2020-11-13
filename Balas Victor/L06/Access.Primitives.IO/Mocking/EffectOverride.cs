using System;
using System.Reflection;

namespace Access.Primitives.IO.Mocking
{
    public class EffectOverride
    {
        public MethodInfo MethodInfo { get; }
        public object Target { get; }
        public object Case { get; }
        public object CaseType { get; }


        public EffectOverride(object @case, MethodInfo methodInfo, object target)
        {
            this.Case = @case;
            CaseType = @case.GetType();
            this.MethodInfo = methodInfo;
            Target = target;
        }
    }
}