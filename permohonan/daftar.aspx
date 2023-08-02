<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="daftar.aspx.cs" Inherits="EPBM.permohonan.daftar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">	
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
  
	<ajaxToolkit:ToolkitScriptManager ID="smPage" runat="server" ScriptMode="Release" />	
    <asp:UpdatePanel ID="upContent" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

	<h1 class="h3 mb-3">DAFTAR <strong>PERMOHONAN</strong></h1>
	<div class="card">
		<div class="card-body">
			 <asp:UpdatePanel ID="upApplicationDetails" runat="server" UpdateMode="Conditional">
				 <ContentTemplate>
			<div class="row">
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">TAJUK <span class="text-danger">*</span></label>						
						 <asp:TextBox ID="txt_tajuk" runat="server" class="form-control" placeholder="TAJUK" required="required"></asp:TextBox>						
					</div>
				</div>
				<div class="col-12 col-lg-6">
					<label class="control-label">JENIS PERTIMBANGAN <span class="text-danger">*</span></label>
					 <asp:DropDownList ID="ddlJenisPertimbangan" class="form-select mb-3" runat="server" DataTextField="Jenis_Pertimbangan" required="required" ></asp:DropDownList>

				</div>
				 <div class="col-12 col-lg-6">
					<label class="control-label">KAEDAH PEROLEHAN <span class="text-danger">*</span></label>
					 <asp:DropDownList ID="ddlKaedahPerolehan" class="form-select mb-3" runat="server" DataTextField="Kaedah_Perolehan" required="required" ></asp:DropDownList>

				</div>
				<div class="col-12 col-lg-6">
					<label class="control-label">JENIS PEROLEHAN / KONTRAK <span class="text-danger">*</span></label>
					 <asp:DropDownList ID="ddlJenisPerolehan" class="form-select mb-3" runat="server" DataTextField="JenisPerolehan" required="required" ></asp:DropDownList>
				</div>
				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">JABATAN <span class="text-danger">*</span></label>
						 <asp:DropDownList ID="ddlJabatan" class="form-select mb-3" runat="server" DataTextField="Jabatan" required="required" ></asp:DropDownList>
					</div>
				</div>

				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">HARGA INDIKATIF / NILAI KONTRAK <span class="text-danger">*</span></label>
						<asp:TextBox ID="txtharga" runat="server" class="form-control mb-3" type="number" step=".01" autocomplete="off" ></asp:TextBox>
<%--					<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                ControlToValidate="txtharga" Display="Dynamic"
                                                ErrorMessage="Sila masukan Nombor shj"
                                                ValidationExpression="^[0-9]+[.][0-9]{2}$"
                                                Font-Bold="True" Font-Italic="True" ForeColor="#CC3300" SetFocusOnError="True"></asp:RegularExpressionValidator>--%>

					</div>
				</div>

				<div class="col-12 col-lg-6">
					<label class="control-label">SUMBER PERUNTUKAN <span class="text-danger">*</span></label>
					  <asp:DropDownList ID="ddlSumberPeruntukan" class="form-select mb-3" runat="server" DataTextField="SumberPeruntukan" required="required" ></asp:DropDownList>
				</div>

				<div class="col-12 col-lg-6">
					<div>
						<label class="control-label">PBM MUKTAMAD <span class="text-danger">*</span></label>		
					  <asp:DropDownList ID="ddlPBMMuktamad" class="form-select mb-3" runat="server" DataTextField="SumberPeruntukan" required="required" ></asp:DropDownList>
					</div>
				</div>
			 
                <div class="col-12 col-lg-6">

                    <label class="control-label">TARIKH SAHLAKU TENDER / KONTRAK <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txttkhsahlaku" runat="server" type="date" required="required" autocomplete="off" onkeydown="return false" class="form-control mb-3" AutoPostBack="true"></asp:TextBox>

                </div>

				<div class="col-12 col-lg-6">
					<label class="control-label">TARIKH TERIMA <span class="text-danger">*</span></label>                                                
                     <asp:TextBox ID="txttkhterima" type="date" runat="server" required="required" autocomplete="off" onkeydown="return false"  class="form-control mb-3"  AutoPostBack="true"></asp:TextBox>                    
                                              
				</div>

				<div class="col-12 col-lg-6">
					<label class="form-check mb-3">
						 <asp:CheckBox ID="cbPerakuan1" runat="server" required="required" />
						
						<span class="form-check-label">
						  LULUS PELAN  PERANCANGAN PEROLEHAN TAHUNAN <span class="text-danger">*</span>
						</span>
					</label>
				</div>
				<div class="col-12">
					<div>
						<label class="control-label">CATATAN</label>
						<asp:TextBox ID="txtcatatan" runat="server" class="form-control mb-3" type="text" autocomplete="off" Width="50%" Height="70px" TextMode="MultiLine"></asp:TextBox>
					</div>
				</div>
				<div class="col-12">
					<asp:Button ID="btnhantar" OnClick="btnHantar_Click" runat="server" class="btn btn-primary" Text="Hantar" CausesValidation="false" />

				</div>

				 <asp:TextBox ID="txttkhcipta" runat="server"  class="form-control" Style="display: none"></asp:TextBox>	
				 <asp:TextBox ID="txticno" runat="server"  class="form-control" Style="display: none"></asp:TextBox>		


			</div>
					  </ContentTemplate>
		 </asp:UpdatePanel>
		</div>
	</div>
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
	<script>
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
</asp:Content>