using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Core.Infrastructure.Database
{
    /// <summary>
    /// Interface que representa uma entidade com um identificador único.
    /// </summary>
    public interface IEntity
    {
        public int Id { get; set; }
    }
}
