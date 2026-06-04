using Org.BouncyCastle.Asn1.Esf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umfgcloud.loja.dominio.service.DTO;

namespace umfgcloud.aplicacao.service.testes.Classes
{
    [TestClass]
    public sealed class ProdutoServicoTestes : AbstractServicoTestes
    {
        private const string C_OWNER = "Leonardo Pereira Trindade Comitre";
        private const string C_CATEGORY = "produto";
        private const decimal C_VALOR_NEGATIVO = -89.90m;
        private const decimal C_VALOR_ZERO = decimal.Zero;

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_Sucesso()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = 89.90m,
                };

                await servico.AdicionarAsync(dto);

                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();

                Assert.IsNotNull(produto);
                Assert.AreNotEqual(Guid.Empty, produto.Id);
                Assert.AreEqual("TESTE", produto.Descricao);
                Assert.AreEqual("123456789", produto.EAN);
                Assert.AreEqual(39.90m, produto.ValorCompra);
                Assert.AreEqual(89.90m, produto.ValorVenda);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaValorCompraNegativo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = -39.90m,
                    ValorVenda = 89.90m,
                };

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaValorVendaNegativo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = -89.90m,
                };

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaValorCompraZero()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = C_VALOR_ZERO,
                    ValorVenda = 89.90m,
                };

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaValorVendaZero()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = C_VALOR_ZERO,
                };

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_SucessoDescricaoConvertidaParaMaiuscula()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "teste minusculo",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = 89.90m,
                };

                await servico.AdicionarAsync(dto);

                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();

                Assert.IsNotNull(produto);
                Assert.AreEqual("TESTE MINUSCULO", produto.Descricao);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_SucessoValoresArredondados()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.999m,
                    ValorVenda = 89.994m,
                };

                await servico.AdicionarAsync(dto);

                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();

                Assert.IsNotNull(produto);
                Assert.AreEqual(40.00m, produto.ValorCompra);
                Assert.AreEqual(89.99m, produto.ValorVenda);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterTodosAsync_Sucesso()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE 1",
                    EAN = "111111111",
                    ValorCompra = 10.00m,
                    ValorVenda = 20.00m,
                });

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE 2",
                    EAN = "222222222",
                    ValorCompra = 30.00m,
                    ValorVenda = 40.00m,
                });

                var produtos = await servico.ObterTodosAsync();

                Assert.IsNotNull(produtos);
                Assert.AreEqual(2, produtos.Count());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterTodosAsync_SucessoVazio()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);

                var produtos = await servico.ObterTodosAsync();

                Assert.IsNotNull(produtos);
                Assert.AreEqual(0, produtos.Count());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterPorIdAsync_Sucesso()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = 89.90m,
                });

                var id = (await servico.ObterTodosAsync()).First().Id;

                var produto = await servico.ObterPorIdAsync(id);

                Assert.IsNotNull(produto);
                Assert.AreEqual(id, produto.Id);
                Assert.AreEqual("TESTE", produto.Descricao);
                Assert.AreEqual("123456789", produto.EAN);
                Assert.AreEqual(39.90m, produto.ValorCompra);
                Assert.AreEqual(89.90m, produto.ValorVenda);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterPorIdAsync_FalhaNaoEncontrado()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);

                await Assert.ThrowsExceptionAsync<ApplicationException>(() => servico.ObterPorIdAsync(Guid.NewGuid()));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_Sucesso()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = 89.90m,
                });

                var id = (await servico.ObterTodosAsync()).First().Id;

                await servico.AtualizarAsync(new ProdutoDTO.ProdutoRequestWithId()
                {
                    Id = id,
                    Descricao = "TESTE ATUALIZADO",
                    EAN = "987654321",
                    ValorCompra = 49.90m,
                    ValorVenda = 99.90m,
                });

                var produto = await servico.ObterPorIdAsync(id);

                Assert.IsNotNull(produto);
                Assert.AreEqual(id, produto.Id);
                Assert.AreEqual("TESTE ATUALIZADO", produto.Descricao);
                Assert.AreEqual("987654321", produto.EAN);
                Assert.AreEqual(49.90m, produto.ValorCompra);
                Assert.AreEqual(99.90m, produto.ValorVenda);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_FalhaNaoEncontrado()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = new ProdutoDTO.ProdutoRequestWithId()
                {
                    Id = Guid.NewGuid(),
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = 89.90m,
                };

                await Assert.ThrowsExceptionAsync<ApplicationException>(() => servico.AtualizarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_FalhaValorCompraNegativo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = 89.90m,
                });

                var id = (await servico.ObterTodosAsync()).First().Id;
                var dto = new ProdutoDTO.ProdutoRequestWithId()
                {
                    Id = id,
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = C_VALOR_NEGATIVO,
                    ValorVenda = 119.90m,
                };

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AtualizarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_FalhaValorVendaNegativo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = 89.90m,
                });

                var id = (await servico.ObterTodosAsync()).First().Id;
                var dto = new ProdutoDTO.ProdutoRequestWithId()
                {
                    Id = id,
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = C_VALOR_NEGATIVO,
                };

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AtualizarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_FalhaValorCompraZero()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = 89.90m,
                });

                var id = (await servico.ObterTodosAsync()).First().Id;
                var dto = new ProdutoDTO.ProdutoRequestWithId()
                {
                    Id = id,
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = C_VALOR_ZERO,
                    ValorVenda = 89.90m,
                };

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AtualizarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_FalhaValorVendaZero()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = 89.90m,
                });

                var id = (await servico.ObterTodosAsync()).First().Id;
                var dto = new ProdutoDTO.ProdutoRequestWithId()
                {
                    Id = id,
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = C_VALOR_ZERO,
                };

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AtualizarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_RemoverAsync_Sucesso()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);

                await servico.AdicionarAsync(new ProdutoDTO.ProdutoRequest()
                {
                    Descricao = "TESTE",
                    EAN = "123456789",
                    ValorCompra = 39.90m,
                    ValorVenda = 89.90m,
                });

                var id = (await servico.ObterTodosAsync()).First().Id;

                await servico.RemoverAsync(id);

                var produtos = await servico.ObterTodosAsync();

                Assert.IsNotNull(produtos);
                Assert.AreEqual(0, produtos.Count());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_RemoverAsync_FalhaNaoEncontrado()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);

                await Assert.ThrowsExceptionAsync<ApplicationException>(() => servico.RemoverAsync(Guid.NewGuid()));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public void ProdutoServico_Instanciar_Falha()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                Assert.ThrowsException<InvalidDataException>(() => GetProdutoServicoInvalidJWT(context));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public void ProdutoServico_Instanciar_Sucesso()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);

                Assert.IsNotNull(servico);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}