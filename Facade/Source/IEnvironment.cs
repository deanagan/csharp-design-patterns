using System;

namespace Facade
{
    public enum EnvironmentTarget
    {
        UNINITIALIZED,
        SANDBOX,
        PRODUCTiON
    }
    public interface IEnvironment
    {
        EnvironmentTarget environmentVariableTarget {get;set;}
    }
}
