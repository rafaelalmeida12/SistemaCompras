using MediatR;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly SolicitacaoAgg.ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly IProdutoRepository _produtoRepository;

        public RegistrarCompraCommandHandler(SolicitacaoAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository, IProdutoRepository produtoRepository, IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
            _produtoRepository = produtoRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand request, CancellationToken cancellationToken)
        {
            var solicitacaoCompra = new SolicitacaoAgg.SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor);
            foreach (var item in request.Itens)
            {
                var produto = _produtoRepository.Obter(item.Id);
                solicitacaoCompra.AdicionarItem(produto,item.Quantidade);
            }
            solicitacaoCompra.RegistrarCompra(solicitacaoCompra.Itens);

            _solicitacaoCompraRepository.RegistrarCompra(solicitacaoCompra);
            Commit();
            PublishEvents(solicitacaoCompra.Events);

            return Task.FromResult(true);
        }
    }
}
