<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="EPBM.mesyuarat.edit_keputusan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-4 text-truncate">KEPUTUSAN <strong><asp:Literal ID="TajukMesyuarat" runat="server"></asp:Literal></strong></h1>
	<div class="card">
		<div class="card-header pb-0">
			<h5 class="card-title">MAKLUMAT PERMOHONAN</h5>
		</div>
		<div class="card-body collapse" id="showDetails">
			<table class="table table-bordered table-hover">
			  <tbody>
				<tr>
					<th scope="row" class="align-middle bg-primary text-white">TAJUK</th>
					<td class=""><asp:Literal ID="LtlTajuk" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JENIS PERTIMBANGAN</th>
				  <td class=""><asp:Literal ID="LtlJenisPertimbangan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">KAEDAH PEROLEHAN</th>
				  <td class=""><asp:Literal ID="LtlKaedahPerolehan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JENIS PEROLEHAN/KONTRAK</th>
				  <td class=""><asp:Literal ID="LtlJenisPerolehan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JABATAN</th>
				  <td class=""><asp:Literal ID="LtlJabatan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">HARGA INDIKATIF / NILAI KONTRAK</th>
				  <td class="">RM <asp:Literal ID="LtlHarga" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">SUMBER PERUNTUKAN</th>
				  <td class=""><asp:Literal ID="LtlSumberPeruntukan" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">TARIKH SAHLAKU TENDER/KONTRAK</th>
				  <td class=""><asp:Literal ID="LtlTarikhSahlaku" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">TARIKH TERIMA</th>
				  <td class=""><asp:Literal ID="LtlTarikhTerima" runat="server"></asp:Literal></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">LULUS PELAN  PERANCANGAN PEROLEHAN TAHUNAN</th>
				  <td class=""><asp:Literal ID="LtlLulusPelan" runat="server"></asp:Literal></td>
				</tr>
			  </tbody>
			</table>
			<div class="show-full text-center">
				<button type="button" class="btn btn-outline-primary rounded-pill" data-bs-toggle="collapse" data-bs-target="#showDetails">
					<span class="btn-before">Papar Penuh <i class="mt-n1" data-feather="chevrons-down"></i ></span>
					<span class="btn-after">Papar Sedikit <i class="mt-n1" data-feather="chevrons-up"></i ></span>
				</button>
			</div>
		</div>
	</div>
	<div class="card">
		<div class="card-body">
			<asp:Panel ID="Panel1" runat="server">
				<div class="row">
					<div class="col-12 mb-2">
						<label class="control-label">STATUS <span class="text-danger">*</span></label>
						<div class="mt-1">
							<asp:RadioButtonList ID="RadioStatus" RepeatDirection="Horizontal" runat="server" CssClass="table table-borderless table-sm mb-0" />
						</div>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Sila Pilih Status" ControlToValidate="RadioStatus" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
					</div>
					<div class="col-12 col-lg-6 mb-2 success">
						<label class="control-label" >SYARIKAT BERJAYA <span class="text-danger">*</span></label>
						<asp:TextBox ID="txtSyarikat" runat="server" CssClass="form-control" placeholder="SYARIKAT BERJAYA" required="required" />
						<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Sila Isi Syarikat Berjaya" ControlToValidate="txtSyarikat" ValidationGroup="success" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
					</div>
					<div class="col-12 col-lg-6 mb-2 success">
						<label class="control-label" >TEMPOH <span class="text-danger">*</span></label>
						<div class="input-group">
							<asp:TextBox ID="txtTempoh" runat="server" CssClass="form-control" TextMode="Number" placeholder="TEMPOH" step="1" min="1" required="required" />
							<span class="input-group-text">BULAN</span>
						</div>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Sila Isi Tempoh" ControlToValidate="txtTempoh" ValidationGroup="success" ForeColor="Red"></asp:RequiredFieldValidator>
						<asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtTempoh" ErrorMessage="Tempoh hanya dalam digit sahaja" ValidationGroup="success" ForeColor="Red" SetFocusOnError="true" />
					</div>
					<div class="col-12 col-lg-6 mb-2 success">
						<label class="control-label">PBM MUKTAMAD <span class="text-danger">*</span></label>
						<asp:DropDownList ID="listPbmMuktamad" CssClass="form-select" required="required" runat="server" /> 
						<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Sila Pilih PBM Muktamad" ControlToValidate="listPbmMuktamad" ValidationGroup="success" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
					</div>
					<div class="col-12 col-lg-6 mb-2 fail">
						<label class="control-label">PBM MUKTAMAD <span class="text-danger">*</span></label>
						<asp:DropDownList ID="listPbmMuktamad2" CssClass="form-select" required="required" runat="server" /> 
						<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Sila Pilih PBM Muktamad" ControlToValidate="listPbmMuktamad2" ValidationGroup="fail" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
					</div>
					<div class="col-12 col-lg-6 mb-2 success">
						<label class="control-label" >TARIKH SURAT SETUJU TERIMA</label>
						<asp:TextBox ID="txtTarikhSetuju" runat="server" CssClass="form-control" TextMode="Date" placeholder="TARIKH SURAT SETUJU TERIMA" />
					</div>
					<div class="col-12 col-lg-6 mb-2 success">
						<label class="control-label" >RUJUKAN SURAT SETUJU TERIMA</label>
						<asp:TextBox ID="txtRujukanSetuju" runat="server" CssClass="form-control" placeholder="RUJUKAN SURAT SETUJU TERIMA" />
					</div>
					<div class="col-12 col-lg-6 mb-2 success">
						<label class="control-label">LAMPIRAN</label>
						<asp:ScriptManager ID="ScriptManager1" runat="server" />
						<div class="d-flex position-relative">
							<asp:Label runat="server" id="attachmentLabel" AssociatedControlId="fileAttachment" class="w-100 preview-file position-absolute border bg-white text-truncate d-flex align-items-stretch" title="168187124571_Screenshot 2023-04-04 110107.jpg">
								<span class="py-1 px-3 bg-body-tertiary border">Choose File</span> 
								<span class="py-1 px-2"><asp:Literal ID="LiteralFileName" runat="server"></asp:Literal></span> 
							</asp:Label>
							<asp:FileUpload ID="fileAttachment" runat="server" CssClass="form-control" placeholder="LAMPIRAN" />
							<button id="removeAttachment" class="m-1 bg-white  position-absolute border-0 top-0 end-0 z-10" title="Remove" type="button"><i class="mt-n1" data-feather="x"></i ></button>
						</div>
						<asp:HiddenField ID="keepAttachment" Value="1" runat="server" />
						<asp:RegularExpressionValidator   
							id="FileUpLoadValidator" runat="server"   
							ErrorMessage="Hanya fail PDF, DOCX, DOC, XLSX, XLS, PPTX, PPT, JPG, PNG dibenarkan."   
							ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.[Pp][Dd][Ff]|.[Dd][Oo][Cc]([Xx]?)|.[Xx][Ll][Ss]([Xx]?)|.[Pp][Pp][Tt]([Xx]?)|.[Jj][Pp][Gg]|.[Pp][Nn][Gg])$"   
							ControlToValidate="fileAttachment"
							ValidationGroup="success"
							ForeColor="Red"   
							SetFocusOnError="true">
						</asp:RegularExpressionValidator> 
					</div>
					<div class="col-12 col-lg-6 mb-2 fail d-none">
						<label class="control-label">LAMPIRAN</label>
						<div class="d-flex position-relative">
							<asp:Label runat="server" id="attachmentLabel2" AssociatedControlId="fileAttachment2" class="w-100 preview-file position-absolute border bg-white text-truncate d-flex align-items-stretch" title="168187124571_Screenshot 2023-04-04 110107.jpg">
								<span class="py-1 px-3 bg-body-tertiary border">Choose File</span> 
								<span class="py-1 px-2"><asp:Literal ID="LiteralFileName2" runat="server"></asp:Literal></span> 
							</asp:Label>
							<asp:FileUpload ID="fileAttachment2" runat="server" CssClass="form-control" placeholder="LAMPIRAN" />
							<button id="removeAttachment2" class="m-1 bg-white  position-absolute border-0 top-0 end-0 z-10" title="Remove" type="button"><i class="mt-n1" data-feather="x"></i ></button>
						</div>
						<asp:HiddenField ID="keepAttachment2" Value="1" runat="server" />
						<asp:RegularExpressionValidator   
							id="FileUpLoadValidator2" runat="server"   
							ErrorMessage="Hanya fail PDF, DOCX, DOC, XLSX, XLS, PPTX, PPT, JPG, PNG dibenarkan."   
							ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.[Pp][Dd][Ff]|.[Dd][Oo][Cc]([Xx]?)|.[Xx][Ll][Ss]([Xx]?)|.[Pp][Pp][Tt]([Xx]?)|.[Jj][Pp][Gg]|.[Pp][Nn][Gg])$"   
							ControlToValidate="fileAttachment2"
							ValidationGroup="fail"
							SetFocusOnError="true"
							ForeColor="Red">  
						</asp:RegularExpressionValidator> 
					</div>
					<div class="col-12 fail mb-2 d-none">
						<div>
							<label class="control-label">ALASAN <span class="text-danger">*</span></label>
							<asp:TextBox ID="txtAlasan" runat="server" CssClass="form-control" placeholder="ALASAN" TextMode="MultiLine" Rows="4" />
							<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Sila Isi Alasan" ControlToValidate="txtAlasan" ValidationGroup="fail" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
						</div>
					</div>
					<div class="col-12">
						<asp:LinkButton ID="btnSubmit" CssClass="btn btn-primary success" ValidationGroup="success" runat="server" OnClick="SaveSuccess">HANTAR</asp:LinkButton>
						<asp:LinkButton ID="btnSubmit2" CssClass="btn btn-primary fail d-none" runat="server" ValidationGroup="fail" OnClick="SaveFail">HANTAR</asp:LinkButton>
					</div>

				</div>
			</asp:Panel>
		</div>
	</div>
	<script>
		var statuses = document.querySelectorAll('#<%=RadioStatus.ClientID %> input[type="radio"]');

		statuses.forEach(status => {
			status.addEventListener('change', function (event) {
				if (status.value == 1 || status.value == 2) {
					failFields = document.querySelectorAll('.fail');
					failFields.forEach(function (field, index) {
						field.classList.add("d-none");
					});
					successFields = document.querySelectorAll('.success');
					successFields.forEach(function (field, index) {
						field.classList.remove("d-none");
					});
				} else {
					successFields = document.querySelectorAll('.success');
					successFields.forEach(function (field, index) {
						field.classList.add("d-none");
					});
					failFields = document.querySelectorAll('.fail');
					failFields.forEach(function (field, index) {
						field.classList.remove("d-none");
					});
				}
			});
            if (status.checked)
            status.dispatchEvent(new Event('change'));
		});

        var fileAttachment = document.getElementById('<%=fileAttachment.ClientID %>');
        fileAttachment.addEventListener('change', function (event) {
            if (fileAttachment.files.length > 0) 
                document.getElementById('<%=attachmentLabel.ClientID %>').classList.add('d-none');
            else if (document.getElementById('<%=LiteralFileName.ClientID %>').innerText.trim() != "")
                document.getElementById('<%=attachmentLabel.ClientID %>').classList.remove('d-none');
		})
		var removeAttachment = document.getElementById('removeAttachment');
		removeAttachment.addEventListener('click', function (event) {
			document.getElementById('<%=attachmentLabel.ClientID %>').classList.add('d-none');
			document.getElementById('<%=fileAttachment.ClientID %>').value = '';
			document.getElementById('<%=keepAttachment.ClientID %>').value = 0;
		})

        var fileAttachment2 = document.getElementById('<%=fileAttachment2.ClientID %>');
        fileAttachment2.addEventListener('change', function (event) {
            if (fileAttachment2.files.length > 0)
                document.getElementById('<%=attachmentLabel2.ClientID %>').classList.add('d-none');
            else if (document.getElementById('<%=LiteralFileName2.ClientID %>').innerText.trim() != "")
                document.getElementById('<%=attachmentLabel2.ClientID %>').classList.remove('d-none');
        })
		var removeAttachment2 = document.getElementById('removeAttachment2');
		removeAttachment2.addEventListener('click', function (event) {
			document.getElementById('<%=attachmentLabel2.ClientID %>').classList.add('d-none');
			document.getElementById('<%=fileAttachment2.ClientID %>').value = '';
			document.getElementById('<%=keepAttachment2.ClientID %>').value = 0;
		})
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
