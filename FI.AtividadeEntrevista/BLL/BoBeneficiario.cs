﻿using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            cli.Alterar(beneficiario);
        }

        /// <summary>
        /// Consulta os beneficiarios baseado no id cliente titular
        /// </summary>
        /// <param name="idCliente">id do cliente</param>
        /// <returns></returns>
        public List<DML.Beneficiario> ConsultarBeneficiariosPorIdCliente(long idCliente)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.ConsultarBeneficiariosPorIdCliente(idCliente);
        }

        /// <summary>
        /// Excluir o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            cli.Excluir(id);
        }     
    }
}
