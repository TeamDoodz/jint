using System.Linq;
using Jint.Runtime.Descriptors;

namespace Jint.Runtime.Interop.Reflection;

internal sealed class MethodAccessor : ReflectionAccessor
{
    private readonly Type _targetType;
    private readonly MethodDescriptor[] _methods;
    private readonly bool _isStatic;

    public MethodAccessor(Type targetType, MethodDescriptor[] methods) : base(null!)
    {
        _targetType = targetType;
        _methods = methods;
        _isStatic = _methods.Any(x => x.Method.IsStatic);
    }

    public override bool Writable => false;

    public override bool IsStatic => _isStatic;

    protected override object? DoGetValue(object? target, string memberName) => null;

    protected override void DoSetValue(object? target, string memberName, object? value)
    {
    }

    public override PropertyDescriptor CreatePropertyDescriptor(Engine engine, object? target, string memberName, bool enumerable = true)
    {
        return new(new MethodInfoFunction(engine, _targetType, target, memberName, _methods), PropertyFlag.AllForbidden);
    }
}
