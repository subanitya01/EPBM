﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="daftar.aspx.cs" EnableEventValidation="false" Inherits="EPBM.SyarikatBerjayaSblm2023.daftar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">	


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">


<script src="../assets/js/jquery.min.js"></script>

<link href="../assets/css/chosen.min.css" rel="stylesheet"/>	
<script src="../assets/js/jquery.chosen.min.js"></script>
<link href="../assets/css/chosen.css" rel="stylesheet" />


<style type="text/css">
        .messagealert {
            width: 100%;
            position: fixed;
             top:0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>

 
	<ajaxToolkit:ToolkitScriptManager ID="smPage" runat="server" ScriptMode="Release" />	
    <asp:UpdatePanel ID="upContent" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          
	<h1 class="h3 mb-3">DAFTAR <strong>MAKLUMAT PEROLEHAN </strong></h1>
	<div class="card">

		<div class="card-body">
			 <asp:UpdatePanel ID="upApplicationDetails" runat="server" UpdateMode="Conditional">
				 <ContentTemplate>

        
            <div id="MessageAlert" runat="server" Visible="false" class="alert alert-warning d-flex align-items-center w-100 alert-outline alert-dismissible" role="alert">
				<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
				<div class="alert-icon me-3">
					<i class="mt-n1" data-feather="bell"></i>
				</div>
				<div class="alert-message">
					 Sila Tandakan pada bahagian lulus pelan perancangan perolehan tahunan !<br>
								 
				</div>
         
			</div>
            <div id="MessageAlertbhg" runat="server" Visible="false" class="alert alert-warning d-flex align-items-center w-100 alert-outline alert-dismissible" role="alert">
				<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
				<div class="alert-icon me-3">
					<i class="mt-n1" data-feather="bell"></i>
				</div>
				<div class="alert-message">
					Sila Lengkapkan maklumat Jabatan / Bahagian !<br>
								 
				</div>
         
			</div>

                     <div class="row">
                         <div class="col-12 col-lg-12">
                             <div>
                                 <label class="control-label">TAJUK <span class="text-danger">*</span></label>
                                 <asp:TextBox ID="txt_tajuk" runat="server" class="form-control mb-3" placeholder="TAJUK" type="text" autocomplete="off" Width="100%" Height="70px" TextMode="MultiLine" required="required"></asp:TextBox>
                             </div>
                         </div>

                    

                        <div>
                      <%-- <br />--%>
                    <div class="row"> 
                    <%--style="padding-bottom: 15px"--%>
                        <div class="col-12 col-lg-6">
    <div>
        <label class="control-label">HARGA INDIKATIF / NILAI KONTRAK <span class="text-danger">*</span></label>
        <asp:TextBox type="hidden" ID="txtharga" runat="server" />
        <asp:TextBox ID="txthargaIn" runat="server" class="form-control mb-3" type="text" step=".01" autocomplete="off" required="required"></asp:TextBox>
        <%--					<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                ControlToValidate="txtharga" Display="Dynamic"
                ErrorMessage="Sila masukan Nombor shj"
                ValidationExpression="^[0-9]+[.][0-9]{2}$"
                Font-Bold="True" Font-Italic="True" ForeColor="#CC3300" SetFocusOnError="True"></asp:RegularExpressionValidator>--%>
    </div>
</div>

                        <div class="col-12 col-lg-6">
                            <label class="control-label">JENIS PEROLEHAN / KONTRAK <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlJenisPerolehan" class="form-select mb-3" runat="server" DataTextField="JenisPerolehan" AutoPostBack="True" OnSelectedIndexChanged="ddlJenisPerolehan_SelectedIndexChanged" required="required"></asp:DropDownList>
                            <asp:Panel ID="PnlJenisPerolehan" runat="server" Visible="false">
                                <asp:TextBox ID="txtJenisPerolehan" runat="server" class="form-control" placeholder="Sila Masukan lain-lain Jenis Perolehan"></asp:TextBox>
                                <br />
                            </asp:Panel>
                        </div>
                    <div>

                    <div class="row">
                        <div class="col-12 col-lg-6">
                            <asp:Label ID ="lblJabatan" runat="server" class="control-label">KEMENTERIAN /JABATAN <span class="text-danger">*</span> </asp:Label>
                           <%-- <label </label>--%>
                            <asp:DropDownList ID="ddlJabatan" runat="server" CssClass="chosen-select" class="form-select mb-3" OnSelectedIndexChanged="ddlJabatan_SelectedIndexChanged" required="required" AutoPostBack="true"></asp:DropDownList>
                            <br />
                        </div>

                        <div class="col-12 col-lg-6">
                        <asp:Panel ID="pnlBahagian" runat="server">
                            <label class="control-label">Bahagian <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlBahagian" runat="server" CssClass="chosen-select" class="form-select mb-3" OnSelectedIndexChanged="ddlBahagian_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </asp:Panel>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        
               

                    </div>
                    <div class="row">                            
 
                        <div class="col-12 col-lg-6">
                            <label class="control-label">TARIKH SAHLAKU TENDER / KONTRAK <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txttkhsahlaku" runat="server" type="date" required="required" autocomplete="off" onkeydown="return false" class="form-control mb-3" AutoPostBack="true"></asp:TextBox>
                        </div>

                        <div class="col-12 col-lg-6">
                            <div>
                                <label class="control-label">TEMPOH(BULAN)<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txttempoh" runat="server" class="form-control mb-3" type="text" step=".01" autocomplete="off" required="required"></asp:TextBox>  
                                
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                ControlToValidate="txttempoh" Display="Dynamic"
                ErrorMessage="Sila masukan Nombor shj"
                ValidationExpression="^[0-9]*$"
                Font-Bold="True" Font-Italic="True" ForeColor="#CC3300" SetFocusOnError="True"></asp:RegularExpressionValidator>

                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ControlToValidate="txttempoh"
                                    ErrorMessage="Sila masukan Nombor shj." ForeColor="Red"
                                    ValidationExpression="^[0-9]*$" Font-Bold="True" Font-Italic="True" ForeColor="#CC3300" SetFocusOnError="True">
                                </asp:RegularExpressionValidator>--%>
                                
                            </div>
                        </div>
                    </div>
  
<br />
                    <div class="row">
                        <div class="col-12 col-lg-6">
                            <div>
                                <label class="control-label">CATATAN</label>
                                <asp:TextBox ID="txtcatatan" runat="server" class="form-control mb-3" type="text" autocomplete="off" Width="100%" Height="70px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>  

                        <div class="col-12 col-lg-6">
                            <div>
                                <label class="control-label">Nama Syarikat</label>
                               <asp:TextBox ID="txtNamaSyarikat" runat="server" class="form-control mb-3" type="text" step=".01" autocomplete="off" required="required"></asp:TextBox>                       
                            </div>
                        </div>  
                    </div>
                    <div class="col-12">
                        <asp:Button ID="btnhantar" OnClick="btnHantar_Click" runat="server" class="btn btn-primary" Text="Hantar" CausesValidation="false" />
                    </div>


                    <asp:TextBox ID="txttkhcipta" runat="server" class="form-control" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txticno" runat="server" class="form-control" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="NamaBahagian" runat="server" class="form-control" Style="display: none"></asp:TextBox>

                     </div>
					  </ContentTemplate>
		 </asp:UpdatePanel>
		</div>
	</div>



 </ContentTemplate>
 </asp:UpdatePanel>

	<script type="text/javascript">
        $(document).ready(function () {
            $(".chosen-select").chosen({ search_contains: true, allow_single_deselect: true });
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $(".chosen-select").chosen({ search_contains: true, allow_single_deselect: true });
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    
    <script src="<%= ResolveUrl("~/assets/js/easy-number-separator.js") %>"></script>
	<script>
        easyNumberSeparator({
            selector: '#<%=txthargaIn.ClientID %>',
            resultInput: '#<%=txtharga.ClientID %>',
        })
	var otherInputs = document.querySelectorAll('.other-input select');
	otherInputs.forEach(otherInput => {
		otherInput.addEventListener('change', function (event) {
			if(otherInput.value=="LAIN-LAIN"){
				otherInput.nextElementSibling.required=true;
				otherInput.nextElementSibling.classList.remove('d-none');
			}
			else{
				otherInput.nextElementSibling.required=false;
				otherInput.nextElementSibling.classList.add('d-none');
			}
		});
		otherInput.dispatchEvent(new Event('change', { 'bubbles': true }));
	});
    </script>


   <script type="text/javascript">
       function ShowMessage(message, messagetype) {
           var cssclass;
           switch (messagetype) {
               case 'Success':
                   cssclass = 'alert-success'
                   break;
               case 'Error':
                   cssclass = 'alert-danger'
                   break;
               case 'Warning':
                   cssclass = 'alert-warning'
                   break;
               default:
                   cssclass = 'alert-info'
           }
           $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
       }
   </script>

</asp:Content>