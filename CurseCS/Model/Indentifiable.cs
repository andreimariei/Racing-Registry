using System;

namespace curse.Model
{
    public interface Indentifiable<ID>
    {
       ID Id { get; set; }
    }
}
