using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Entities;

public interface IEntity<T>
{
    public T ToRecord();
}
