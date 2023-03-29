using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class ItemCompraCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }
    }
}
