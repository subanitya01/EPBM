<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="papar-pengesahan.aspx.cs" Inherits="EPBM.permohonan.papar_pengesahan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
		<h1 class="h3 mb-4 text-truncate">PERMOHONAN: <strong> <asp:Label ID="lblTajukUtama" runat="server" ></asp:Label></strong></h1>
	<div class="card">
		<div class="card-body">

			<div class="btn-group btn-group-sm mb-3 float-left" role="group">
			<a href="/permohonan/senarai-pengesahan.aspx" class="btn btn-secondary" style=text-align:left>Back</a>			
			</div>
			
			<div class="btn-group btn-group-sm mb-3 float-end" role="group">
			
				<a href="#" data-bs-toggle="modal" data-bs-target="#approveModal" class="btn btn-primary"><i class="mt-n1" data-feather="check"></i> SAHKAN</a>
				<a href="#" data-bs-toggle="modal" data-bs-target="#rejectModal" class="btn btn-warning"><i class="mt-n1" data-feather="x"></i> KEMBALIKAN</a>
			</div>
			<table class="table table-bordered table-hover">
			  <tbody>
				<tr>
				  <th scope="row" class="align-middle bg-primary text-white">TAJUK</th>
				  <td class=""><strong><asp:Label ID="lblTajuk" runat="server" ></asp:Label></strong></td>
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
		</div>
	</div>
	<div class="modal fade" id="approveModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-primary">
					<h5 class="modal-title text-white text-truncate">SAHKAN PERMOHONAN</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<p class="mb-0">Anda pasti untuk mengesahkan permohonan ini?</p>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<asp:LinkButton ID="btnhapus" runat="server"  OnClick="btnSah_Click" Text="SAHKAN" class="btn btn-primary" > </asp:LinkButton>					
				</div>
			</div>
		</div>
	</div>
	<div class="modal fade" id="rejectModal" data-bs-backdrop="static" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-warning">
					<h5 class="modal-title text-white text-truncate">KEMBALIKAN PERMOHONAN</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				
				<div class="modal-body m-3">
					
			    <p class="">Anda pasti untuk bawa kembalikan permohonan ini untuk pengemaskinian?</p>
			    <div id="MessageAlert" runat="server" Visible="false" class="alert alert-warning d-flex align-items-center w-100 alert-outline alert-dismissible" role="alert">
				<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
				<div class="alert-icon me-3">
					<i class="mt-n1" data-feather="bell"></i>
				</div>
				<div class="alert-message">
					Sila Lengkapkan ruangan catatan!<br>								 
				</div>
         
			   </div>
					<div>
						 <asp:TextBox ID="txtcatatanpengesah" runat="server" required="required" Width="100%" Height="100px" TextMode="MultiLine" placeholder="CATATAN" class="form-control mb-3" rows="4" ></asp:TextBox>
						<asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="Country is required!" ControlToValidate="txtcatatanpengesah" Display="Dynamic" ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
						
						
					</div>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<asp:LinkButton ID="btnKembali" runat="server"  OnClick="btnKembali_Click" Text="KEMBALIKAN" class="btn btn-warning"> </asp:LinkButton>						
				</div>
			</div>
		</div>
	</div>


 <script type="text/javascript">
     {
         $('#rejectModal').modal('show'); //display something
         //...

         // if you don't want to lose the reference to previous backdrop
         $('#rejectModal').modal('hide');
         $('#rejectModal').data('bs.modal', null); // this clears the BS modal data
         //...
         // now works as you would expect
         $('#rejectModal').modal({ backdrop: 'static', keyboard: false });

     }

 </script>


<script type="text/javascript">

    function openrejectModal() {
        var myModal = new bootstrap.Modal(document.getElementById('rejectModal'), { keyboard: false }, { backdrop: 'static'});
        myModal.show();
    }
</script>
</asp:Content>





<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
