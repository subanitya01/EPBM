<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" EnableEventValidation="false"  Inherits="EPBM.permohonan.edit" %>
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
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>

 
	<ajaxToolkit:ToolkitScriptManager ID="smPage" runat="server" ScriptMode="Release" />	
    <asp:UpdatePanel ID="upContent" runat="server" UpdateMode="Conditional">
 
        
        <ContentTemplate>   
            
	<h1 class="h3 mb-3">DAFTAR <strong>MAKLUMAT PEROLEHAN</strong></h1>          
	<div class="card">
       
		<div class="card-body">
           
			 <asp:UpdatePanel ID="upApplicationDetails" runat="server" UpdateMode="Conditional">
				 <ContentTemplate>
                     
                     <div id="MessageAlert" runat="server" visible="false" class="alert alert-warning d-flex align-items-center w-100 alert-outline alert-dismissible" role="alert">
                         <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                         <div class="alert-icon me-3">
                             <i class="mt-n1" data-feather="bell"></i>
                         </div>
                         <div class="alert-message">
                             Sila Tandakan pada bahagian lulus pelan perancangan perolehan tahunan !<br>
                         </div>
                     </div>

                     <div id="MessageAlertbhg" runat="server" visible="false" class="alert alert-warning d-flex align-items-center w-100 alert-outline alert-dismissible" role="alert">
                         <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                         <div class="alert-icon me-3">
                             <i class="mt-n1" data-feather="bell"></i>
                         </div>
                         <div class="alert-message">
                             Sila Lengkapkan maklumat Jabatan / bahagian !<br>
                         </div>
                     </div>


                     <div class="row">             
                         <div class="col-12">
                             <div>                                 
                                 <label class="control-label">TAJUK <span class="text-danger">*</span></label>
                                 <asp:TextBox ID="txt_tajuk" runat="server" class="form-control" placeholder="TAJUK" type="text" autocomplete="off" Width="100%" Height="70px" TextMode="MultiLine" required="required"></asp:TextBox>
                             </div>
                         </div>
                                            
                        <div>
                       <br />
                    <div class="row"> 
                    <%--style="padding-bottom: 15px"--%>                                
                        <div class="col-12 col-lg-6">
                            <label class="control-label">JENIS PERTIMBANGAN <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlJenisPertimbangan" class="form-select mb-3" runat="server" DataTextField="Jenis_Pertimbangan" AutoPostBack="True" OnSelectedIndexChanged="ddlJenisPertimbangann_SelectedIndexChanged" required="required"></asp:DropDownList>
                            <asp:Panel ID="PnlJenisPertimbangan" runat="server" Visible="false">
                                <asp:TextBox ID="txtJenisPertimbangan" runat="server" class="form-control" placeholder="Sila Masukan lain-lain Jenis Pertimbangan"></asp:TextBox>
                                <br />
                            </asp:Panel>
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
                        <asp:Panel ID="pnlBahagian"  Visible="false" runat="server">
                            <label class="control-label">Bahagian <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlBahagian" runat="server" CssClass="chosen-select" class="form-select mb-3" OnSelectedIndexChanged="ddlBahagian_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </asp:Panel>
                        </div>
                    </div>
                    <br />
                    <div class="row">
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
                            <div>
                                <label class="control-label">PBM MUKTAMAD <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPBMMuktamad" CssClass="chosen-select" class="form-select mb-3" runat="server" required="required"></asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="row">                            
                        <div class="col-12 col-lg-6">
                            <label class="control-label">TARIKH TERIMA <span class="text-danger">*</span></label>
                           <%-- <asp:TextBox ID="txttkhterima"  Text='<%# Bind("Date", "{0:dd-MM-yyyy}") %>' runat="server" type ='date' required="required" autocomplete="off" onkeydown="return false" class="form-control mb-3" AutoPostBack="true"></asp:TextBox>--%>
                        <asp:TextBox ID="txttkhterima"  runat="server" type ='date' required="required" autocomplete="off" onkeydown="return false" class="form-control mb-3" AutoPostBack="true"></asp:TextBox>
                            </div>
                        <div class="col-12 col-lg-6">
                            <label class="control-label">TARIKH SAHLAKU TENDER / KONTRAK <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txttkhsahlaku" runat="server" required="required" type ='date' autocomplete="off" onkeydown="return false" class="form-control mb-3" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">                                
                        <div class="col-12 col-lg-6">
                            <label class="control-label">SUMBER PERUNTUKAN <span class="text-danger">*</span></label>
                            <asp:DropDownList ID="ddlSumberPeruntukan" class="form-select mb-3" runat="server" DataTextField="SumberPeruntukan" AutoPostBack="True" OnSelectedIndexChanged="ddlSumberPeruntukan_SelectedIndexChanged" required="required"></asp:DropDownList>
                            <asp:Panel ID="PnlSumberPeruntukan" runat="server" Visible="false">
                                <asp:TextBox ID="txtSumberPeruntukan" runat="server" class="form-control" placeholder="Sila Masukan lain-lain Sumber Peruntukan"></asp:TextBox>
                                <br />
                            </asp:Panel>
                        </div>
                        <div class="col-12 col-lg-6">
                            <label class="form-check mb-3"  style="padding-top: 20px;padding-left: 10px;">
                                <asp:CheckBox ID="cbPerakuan1" runat="server" onclick="return false" />
                                <span class="form-check-label">LULUS PELAN  PERANCANGAN PEROLEHAN TAHUNAN <span class="text-danger">*</span>
                                </span>
                            </label>
                        </div>

                          <div class="col-12 col-lg-6">
                             <label class="control-label">KAEDAH PEROLEHAN <span class="text-danger">*</span></label>
                             <asp:DropDownList ID="ddlKaedahPerolehan" CssClass="chosen-select" class="form-select mb-3" runat="server" DataTextField="Kaedah_Perolehan" required="required"></asp:DropDownList>
                         </div> 
                    </div>
<br />
                    <div class="row">
                        <div class="col-12">
                            <div>
                                <label class="control-label">CATATAN</label>
                                <asp:TextBox ID="txtcatatan" runat="server" class="form-control mb-3" type="text" autocomplete="off" Width="49%" Height="70px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        
                        <asp:Panel ID="pnlCatatanPengesah" Visible="false" runat="server">
                            <div class="col-12">
                                <div>
                                    <label class="control-label">CATATAN PENGESAH</label>
                                    <asp:TextBox ID="txtCatatanPengesah" runat="server" class="form-control mb-3" type="text" autocomplete="off" Width="49%" Height="70px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
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
	  <script type="text/javascript">
          Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
              FUNCTION();
          });
      </script> 
    <script src="<%= ResolveUrl("~/assets/js/easy-number-separator.js") %>"></script>
	<script>
        easyNumberSeparator({
            selector: '#<%=txthargaIn.ClientID %>',
         resultInput: '#<%=txtharga.ClientID %>',
     })
        var otherInputs = document.querySelectorAll('.other-input select');
        otherInputs.forEach(otherInput => {
            otherInput.addEventListener('change', function (event) {
                if (otherInput.value == "LAIN-LAIN") {
                    otherInput.nextElementSibling.required = true;
                    otherInput.nextElementSibling.classList.remove('d-none');
                }
                else {
                    otherInput.nextElementSibling.required = false;
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
