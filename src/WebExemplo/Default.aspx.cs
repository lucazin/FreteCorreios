﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebExemplo
{
    public partial class Default : System.Web.UI.Page
    {
        FreteCorreios.FreteCorreiosWS FC;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetWS_Click(object sender, EventArgs e)
        {
            FC = new FreteCorreios.FreteCorreiosWS();

            var Res = FC.CalcPrecoPrazo(
                tbCodEmpresa1.Text,             //--=> Código da empresa para que tem contrato com o Correios.
                tbSenha1.Text,                  //--=> Utilizado junco com o contrato.
                ddlServico1.SelectedValue,      //--=> Número do serviço.
                tbCepO1.Text,                   //--=> CEP Origem.
                tbCepD1.Text,                   //--=> CEP Destino.
                tbPeso1.Text,                   //--=> Peso da encomenda, incluindo sua embalagem. O peso deve ser informado em quilogramas.
                int.Parse(ddlFormato1.SelectedValue), //--=> Formato.
                decimal.Parse(tbCompr1.Text),   //--=> Comprimento da encomenda (incluindo embalagem), em centímetros.
                decimal.Parse(tbAltur1.Text),   //--=> Altura da encomenda (incluindo embalagem), em centímetros.
                decimal.Parse(tbLarg1.Text),    //--=> Largura da encomenda (incluindo embalagem), em centímetros.
                decimal.Parse(tbDiam1.Text),    //--=> Diâmetro da encomenda (incluindo embalagem), em centímetros.
                ddlMaoP1.SelectedValue,         //--=> Mão própria. Valores possíveis: S ou N.
                decimal.Parse(tbValD1.Text),    //--=> Valor declarado. Deve ser apresentado o valor declarado em Reais ou 0 para não declarado.
                ddlAvisoR1.SelectedValue        //--=> Aviso de Recebimento. Valores possíveis: S ou N.
                );

            lblRes1.Text = "";

            lblRes1.Text = AllCorreiosResultado(Res);

            AllFuncion();
        }

        private void AllFuncion()
        {
            lblAllService.Text = "<u>CalcPrazo</u><br/>" + AllCorreiosResultado(FC.CalcPrazo(ddlServico1.SelectedValue, tbCepO1.Text, tbCepD1.Text)) + "<br/><br/>";

            lblAllService.Text += "<u>CalcPrazoData</u><br/>" + AllCorreiosResultado(FC.CalcPrazoData(ddlServico1.SelectedValue, tbCepO1.Text, tbCepD1.Text, DateTime.Now.Date.ToShortDateString())) + "<br/><br/>";

            lblAllService.Text += "<u>CalcPrazoRestricao</u><br/>" + AllCorreiosResultado(FC.CalcPrazoRestricao(ddlServico1.SelectedValue, tbCepO1.Text, tbCepD1.Text, DateTime.Now.Date.ToShortDateString())) + "<br/><br/>";

            lblAllService.Text += "<u>CalcPrazoRestricao</u><br/>" + AllCorreiosResultado(FC.CalcPreco(tbCodEmpresa1.Text, tbSenha1.Text, ddlServico1.SelectedValue, tbCepO1.Text, tbCepD1.Text, tbPeso1.Text, int.Parse(ddlFormato1.SelectedValue), decimal.Parse(tbCompr1.Text), decimal.Parse(tbAltur1.Text), decimal.Parse(tbLarg1.Text), decimal.Parse(tbDiam1.Text), ddlMaoP1.SelectedValue, decimal.Parse(tbValD1.Text), ddlAvisoR1.SelectedValue)) + "<br/><br/>";

            lblAllService.Text += "<u>CalcPrecoPrazoRestricao</u><br/>" + AllCorreiosResultado(FC.CalcPrecoPrazoRestricao(tbCodEmpresa1.Text, tbSenha1.Text, ddlServico1.SelectedValue, tbCepO1.Text, tbCepD1.Text, tbPeso1.Text, int.Parse(ddlFormato1.SelectedValue), decimal.Parse(tbCompr1.Text), decimal.Parse(tbAltur1.Text), decimal.Parse(tbLarg1.Text), decimal.Parse(tbDiam1.Text), ddlMaoP1.SelectedValue, decimal.Parse(tbValD1.Text), ddlAvisoR1.SelectedValue, DateTime.Now.Date.ToShortDateString())) + "<br/><br/>";
        }

        private string  AllCorreiosResultado(FreteCorreios.wsCalculaPrecoPrazo.cResultado Res)
        {
            if (!string.IsNullOrEmpty(Res.Servicos[0].MsgErro))
                return Res.Servicos[0].MsgErro;
            else
            {
                string ret = "";

                foreach (var x1 in Res.Servicos.ToList())
                    foreach (var x2 in x1.GetType().GetProperties())
                    {
                        try
                        {
                            ret += x2.Name + "  =  " + x2.GetValue(x1).ToString() + "<br/>";
                        }
                        catch { };

                    }
                return ret;
            }
        }
    }
}