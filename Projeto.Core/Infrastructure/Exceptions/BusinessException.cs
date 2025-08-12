using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Core.Infrastructure.Exceptions
{
    /// <summary>
    /// Classe de exceção personalizada para erros de negócio.
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}
