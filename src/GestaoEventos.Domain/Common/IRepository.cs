﻿namespace GestaoEventos.Domain.Common;

public interface IRepository<T>
    where T : IAggregateRoot
{
}