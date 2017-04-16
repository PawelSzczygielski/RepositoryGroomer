using System;

namespace RepositoryGroomer.Modern
{
    /// <summary>
    /// Just to indicate that method is to be invoked by internal Caliburn.Micro mechanism.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CaliburnMicroActionTargetAttribute : Attribute
    {

    }
}