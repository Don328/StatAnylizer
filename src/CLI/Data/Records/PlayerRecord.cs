using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatAnylizer.Core.Data.Records;

public record PlayerRecord(
    int Id,
    int TeamId,
    string FirstName,
    string LastName);

