using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Erros
{
    public class BadRequest : Error
    {
        public BadRequest(string mensagem) : base(mensagem)
        {
        }
    }
}
