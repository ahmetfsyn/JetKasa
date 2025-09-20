using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetKasa.Domain.Abstractions;

public abstract class Entity
{
    public Entity()
    {
        Id = Guid.CreateVersion7();
    }
    public Guid Id { get; set; }
}
