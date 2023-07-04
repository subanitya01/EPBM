<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DaftarPerolehan.aspx.cs" Inherits="EPBM.DaftarPerolehan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style>
 
    .title {
    text-align: center;
    padding: 10px 0;
    font-family: Arial;
    color: darkblue;
    font-size: 24px;
    font-weight: normal;
    margin-top: 8px;
    margin-bottom: 7px;
    background-color: #b6d4fe;
    height: 46px;
}

 
 label {
    display: inline-block;
    max-width: 100%;
    margin-bottom: 15px;
    font-weight: 100;
    font-size :14px;
    text-align :end
}

.accordion-button {
   text-align: right;
    align-items: center;
    width: 100%;
    padding: 0.5rem 10px;
    font-size: 16px;
    /*padding: 1rem 15px;
    font-size: 2rem;*/


}
  #main {
    margin-top: 0px;
    padding: 20px 30px;
    transition: all 0.3s;
    }

    </style>

<link href="assets/css/Style_Form.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="main" class="main">


    <section class="wrapper">

        <%--<header class="panel-heading"></header>--%>
        <h4 class="title">CHANGE REQUEST FORM</h4>
        <div class="panel-group" id="accordion">
            <div class="panel panel-border">
                <%--<div class="panel-heading">--%>
                <%--   <h4 class="panel-title">--%>
                <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseOne" aria-expanded="true" aria-controls="panelsStayOpen-collapseOne">
                    MAKLUMAT PEGAWAI PEMOHON
                </button>

                <%-- </div>--%>
                <div id="panelsStayOpen-collapseOne" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingOne">
                    <div class="panel-body">

                        <div class="row">
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Nama Pegawai </label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="txtnama" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>
                            </div>

                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Bahagian </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBox1" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*NO IC </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txticno" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>


                            </div>
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*No Tel (Pej) </label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="txtnopej" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Jawatan </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBox2" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>


                            </div>
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*No Tel (Hp) </label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="txtnotelhp" runat="server" required="required" class="input-sm form-control" ReadOnly="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Gred </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBox3" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>

                            </div>
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Alamat Emel </label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="txtemelrasmi" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="panel panel-border">
                <%--<div class="panel-heading">--%>
                <%--   <h4 class="panel-title">--%>
                <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapsetwo" aria-expanded="true" aria-controls="panelsStayOpen-collapsetwo">
                    MAKLUMAT PEGAWAI PEMOHON
                </button>

                <%-- </div>--%>
                <div id="panelsStayOpen-collapsetwo" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingtwo">
                    <div class="panel-body">

                        <div class="row">
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Nama Pegawai </label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="TextBox4" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>
                            </div>

                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Bahagian </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBox5" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*NO IC </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBox6" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>


                            </div>
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*No Tel (Pej) </label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="TextBox7" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Jawatan </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBox8" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>


                            </div>
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*No Tel (Hp) </label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="TextBox9" runat="server" required="required" class="input-sm form-control" ReadOnly="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Gred </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBox10" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>

                            </div>
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Alamat Emel </label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="TextBox11" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="panel panel-border">
                <%--<div class="panel-heading">--%>
                <%--   <h4 class="panel-title">--%>
                <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapsethree" aria-expanded="true" aria-controls="panelsStayOpen-collapsethree">
                    MAKLUMAT PEGAWAI PEMOHON
                </button>

                <%-- </div>--%>
                <div id="panelsStayOpen-collapsethree" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingthree">
                    <div class="panel-body">

                        <div class="row">
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Nama Pegawai </label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="TextBox12" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>
                            </div>

                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Bahagian </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBox13" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*NO IC </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBox14" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>


                            </div>
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*No Tel (Pej) </label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="TextBox15" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Jawatan </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBox16" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>


                            </div>
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*No Tel (Hp) </label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="TextBox17" runat="server" required="required" class="input-sm form-control" ReadOnly="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Gred </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="TextBox18" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>

                            </div>
                            <label class="col-xs-2 control-label no-padding-right" for="form-field-1">*Alamat Emel </label>
                            <div class="col-sm-3">

                                <asp:TextBox ID="TextBox19" runat="server" required="required" class="input-sm form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    <!-- Button trigger modal -->
<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
  Launch demo modal
</button>

<!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        ...
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>
           </div> 

 
    </section>
</div>


</asp:Content>
