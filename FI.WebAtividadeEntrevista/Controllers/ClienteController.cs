using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Utils;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            BoBeneficiario boBeneficiarios = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                // Verifica se o CPF é valido
                if (!DocumentoUtils.IsCpfValido(model.CPF))
                {
                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, "Digito verificador do CPF inválido"));
                }

                // Verifica se o CPF não foi inserido para algum cliente
                if (!bo.PodeUsarEsseCpf(0, model.CPF))
                {
                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, "CPF informado já cadastrado para outro cliente"));
                }

                // Validação de Beneficiarios
                // CPF duplicado
                // if (model.Beneficiarios != null && model.Beneficiarios.Select(x => x.CPF).GroupBy(x => x).Any(g => g.Count() > 1))
                if (model.Beneficiarios != null && model.Beneficiarios.Count() != model.Beneficiarios.Select(x => x.CPF).Distinct().Count())
                {
                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, "CPF duplicado para beneficiário"));
                }

                // TODO: Validar se o CPF do beneficiário não cadastrado antes para outro cliente???

                // Cliente
                model.Id = bo.Incluir(new Cliente()
                {
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                });

                // Beneficiarios
                // Foi feito cadastro de beneficiarios para o cliente titular
                if (model.Beneficiarios != null)
                {
                    foreach (var beneficiario in model.Beneficiarios)
                    {
                        boBeneficiarios.Incluir(new Beneficiario()
                        {
                            Id = beneficiario.Id,
                            CPF = beneficiario.CPF,
                            Nome = beneficiario.Nome,
                            IdCliente = model.Id
                        });
                    }
                }

                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            BoBeneficiario boBeneficiarios = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                // Verifica se o CPF é valido
                if (!DocumentoUtils.IsCpfValido(model.CPF))
                {
                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, "Digito verificador do CPF inválido"));
                }

                // Verifica se o CPF não foi inserido para algum cliente
                if (!bo.PodeUsarEsseCpf(model.Id, model.CPF))
                {
                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, "CPF informado já cadastrado para outro cliente"));
                }

                // Validação de Beneficiarios
                // CPF duplicado
                // if (model.Beneficiarios != null && model.Beneficiarios.Select(x => x.CPF).GroupBy(x => x).Any(g => g.Count() > 1))
                if (model.Beneficiarios != null && model.Beneficiarios.Count() != model.Beneficiarios.Select(x => x.CPF).Distinct().Count())
                {
                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, "CPF duplicado para beneficiário"));
                }

                // TODO: Validar se o CPF do beneficiário não cadastrado antes para outro cliente???

                // Cliente
                bo.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                });

                // Beneficiarios
                // Verifica quantos beneficiarios existiam antes para o cliente titular
                var beneficiariosInDb = boBeneficiarios.ConsultarBeneficiariosPorIdCliente(model.Id);                

                if (beneficiariosInDb != null && beneficiariosInDb.Any())
                {
                    // Separa os CPFs dos beneficiarios no banco, usado dessa forma para NÃO ter que consultar novamente
                    // caso algum beneficiario seja excluido
                    var beneficiariosCpfsInDb = beneficiariosInDb.Select(x => x.CPF).ToList();

                    // Existia beneficiarios cadastrados, mas foram excluidos todos
                    if (model.Beneficiarios == null)
                    {
                        foreach (var beneficiario in beneficiariosInDb)
                        {
                            boBeneficiarios.Excluir(beneficiario.Id);
                        }
                    }
                    else
                    {
                        var modelBeneficiariosCpfs = model.Beneficiarios.Select(x => x.CPF).ToList();
                        foreach (var beneficiarioInDb in beneficiariosInDb)
                        {
                            // Excluir apenas os beneficiarios que não estão mais na lista vindos do frontend
                            if (!modelBeneficiariosCpfs.Contains(beneficiarioInDb.CPF)) 
                            {
                                boBeneficiarios.Excluir(beneficiarioInDb.Id);
                                beneficiariosCpfsInDb.Remove(beneficiarioInDb.CPF); // Remove o cpf da lista, para manter os valores atualizados
                            }
                        }

                        // Foi feito cadastro/alteração de beneficiarios para o cliente titular                        
                        foreach (var modelBeneficiario in model.Beneficiarios)
                        {
                            if (!beneficiariosCpfsInDb.Contains(modelBeneficiario.CPF))
                            {
                                boBeneficiarios.Incluir(new Beneficiario()
                                {
                                    Id = modelBeneficiario.Id,
                                    CPF = modelBeneficiario.CPF,
                                    Nome = modelBeneficiario.Nome,
                                    IdCliente = model.Id
                                });
                            }
                            else
                            {
                                boBeneficiarios.Alterar(new Beneficiario()
                                {
                                    Id = modelBeneficiario.Id,
                                    CPF = modelBeneficiario.CPF,
                                    Nome = modelBeneficiario.Nome,
                                    IdCliente = model.Id
                                });
                            }                            
                        }
                    }
                }
                else
                {
                    // Foi feito cadastro de beneficiarios para o cliente titular
                    if (model.Beneficiarios != null)
                    {
                        foreach (var beneficiario in model.Beneficiarios)
                        {
                            boBeneficiarios.Incluir(new Beneficiario()
                            {
                                Id = beneficiario.Id,
                                CPF = beneficiario.CPF,
                                Nome = beneficiario.Nome,
                                IdCliente = model.Id
                            });
                        }
                    }
                }

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF,
                    Beneficiarios = new List<BeneficiarioModel>()
                };

                // Popula os beneficiarios do cliente titular
                BoBeneficiario boBeneficiario = new BoBeneficiario();
                var beneficiarios = boBeneficiario.ConsultarBeneficiariosPorIdCliente(id);

                foreach (var beneficiario in beneficiarios)
                {
                    model.Beneficiarios.Add(new BeneficiarioModel
                    {
                        Id = beneficiario.Id,
                        CPF = beneficiario.CPF,
                        Nome = beneficiario.Nome,
                        IdCliente = model.Id
                    });
                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}