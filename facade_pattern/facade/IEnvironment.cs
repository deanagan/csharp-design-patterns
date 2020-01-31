using System;

namespace facade
{
    public enum EnvironmentTarget
    {
        SANDBOX,
        PRODUCTiON
    }
    public interface IEnvironment
    {
        EnvironmentTarget environmentVariableTarget {get;set;}
    }
}
