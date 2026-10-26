using System;
using System.Collections.Generic;

namespace Memento;

public sealed class LoanScenarioHistory
{
    private readonly List<LoanScenarioMemento> _mementos = [];

    public int Count => _mementos.Count;

    public void Save(LoanScenarioMemento memento)
    {
        ArgumentNullException.ThrowIfNull(memento);
        _mementos.Add(memento);
    }

    public LoanScenarioMemento Get(int index) => _mementos[index];

    public LoanScenarioMemento GetLatest()
    {
        if (_mementos.Count == 0)
        {
            throw new InvalidOperationException("No loan scenarios have been saved.");
        }

        return _mementos[^1];
    }
}
