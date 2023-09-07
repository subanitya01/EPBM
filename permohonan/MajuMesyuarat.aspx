<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MajuMesyuarat.aspx.cs" Inherits="EPBM.permohonan.MajuMesyuarat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<div class="btn-group btn-group-sm mb-3 float-end" role="group">
			<a href="/permohonan/senarai.aspx" class="btn btn-secondary" style=text-align:right; width="1px">Back</a>			
			</div>
	<h1 class="h3 mb-4 text-truncate">PERMOHONAN: <strong> <asp:Label ID="lblTajukUtama" runat="server" ></asp:Label></strong></h1>

	<asp:TextBox ID="ID_Meeting" style="display:none" runat="server"  type="text" ></asp:TextBox> 
	
	<div class="card">

		
		<div class="card-header pb-0">
			<h5 class="card-title">MAKLUMAT PERMOHONAN</h5>
		</div>
		
		<div class="card-body collapse" id="showDetails">
			<table class="table table-bordered table-hover">
		<tbody>			
				 <tr>
				  <th scope="row" class="align-middle bg-primary text-white">TAJUK</th>
				  <td class=""><asp:Label ID="lblTajuk" runat="server" ></asp:Label></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JENIS PERTIMBANGAN</th>
				  <td class=""><asp:Label ID="lblJPertimbangan" runat="server" ></asp:Label>	<br />				   					     					   
                              <asp:Label ID="lblLainLainJPertimbangan" Visible="false" runat="server" ></asp:Label>
                    
				  </td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">KAEDAH PEROLEHAN</th>
				  <td class=""><asp:Label ID="lblKaedahPerolehan" runat="server" ></asp:Label></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JENIS PEROLEHAN/KONTRAK</th>
				  <td class=""><asp:Label ID="lblJenisPerolehan_Kontrak" runat="server" ></asp:Label><br />					  
                             <asp:Label ID="lblLainLainJPerolehan" runat="server" Visible="false"></asp:Label>
                   
				</tr>				  	 
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">JABATAN</th>
				  <td class=""><asp:Label ID="lblJabatan" runat="server" ></asp:Label></td>
				 
				</tr>

				<asp:Panel ID="PnlBahagian" runat="server" Visible="false">
				  <tr>
				  <th scope="row" class="align-middle bg-primary text-white">BAHAGIAN</th>
				  <td class=""><asp:Label ID="lblBahagian" runat="server" ></asp:Label></td>
				 
				</tr>
				</asp:Panel>

				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">HARGA INDIKATIF / NILAI KONTRAK</th>
				  <td class="">RM <asp:Label ID="lblHargaIndikatif" runat="server" ></asp:Label></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">SUMBER PERUNTUKAN</th>
				  <td class=""><asp:Label ID="lblSumberPeruntukan" runat="server" ></asp:Label><br />	
                              <asp:Label ID="lblLainLainSPeruntukan" runat="server" Visible="false"></asp:Label>
				  </td>
				 
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">PBM MUKTAMAD</th>
				  <td class=""><asp:Label ID="lblPBM_Muktamad" runat="server" ></asp:Label></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">TARIKH SAHLAKU TENDER/KONTRAK</th>
				  <td class=""><asp:Label ID="TarikhSahlaku" runat="server" ></asp:Label></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">TARIKH TERIMA</th>
				  <td class=""><asp:Label ID="lblTarikhTerima" runat="server" ></asp:Label></td>
				</tr>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">LULUS PELAN  PERANCANGAN PEROLEHAN TAHUNAN</th>
				  <td class=""><asp:Label ID="lblLulus_Pelan" runat="server" ></asp:Label></td>
				</tr>
				  <tr>
				  <th scope="row" class="align-middle bg-primary text-white">Catatan</th>
				  <td class=""><asp:Label ID="lblCatatan" runat="server" ></asp:Label></td>
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
			<div class="row">
				
				<div class="col-12 col-lg-6 success">
					<div>
						 <label class="control-label">Majukan Ke Mesyuarat<span class="text-danger">*</span></label>
                            
                             <asp:DropDownList ID="ddlMeeting" class="form-select mb-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMeeting_SelectedIndexChanged" required="required"></asp:DropDownList>
					</div>
				</div>

			</div>
		</div>
	</div>

	<div class="col-12">
                        <asp:Button ID="btnhantar" OnClick="btnHantar_Click" runat="server" class="btn btn-primary" Text="Hantar" CausesValidation="false" />
                    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
	<script>
	var statuses = document.querySelectorAll('input[name="inlineRadioOptions"]');
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
    });
	
	var removeAttachment = document.getElementById('removeAttachment');
	removeAttachment.addEventListener('click', function (event) {
		document.getElementById('attachmentLabel').classList.add('d-none');
		document.getElementById('attachment').value = '';
		document.querySelector('input[name="keepAttachment"]').value = 0;
	})
    </script>
</asp:Content>