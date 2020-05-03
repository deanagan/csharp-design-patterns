using System;

namespace facade
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
