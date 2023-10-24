<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="daftar.aspx.cs" Inherits="EPBM.mesyuarat.daftar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<h1 class="h3 mb-3">
	SENARAI <strong>MESYUARAT BERDAFTAR</strong>
	
	<span class="btn-group btn-group-sm float-end" role="group">
		<a class="btn btn-success" data-bs-toggle="collapse" href="#addMeeting" role="button" aria-expanded="false" aria-controls="add" title="Tambah Mesyuarat Baru"><i class="align-middle" data-feather="plus"></i> Mesyuarat Baru</a>
	</span>
</h1>
	<div id="addMeeting" class="card collapse">
		<div class="card-header pb-0">
			<h5 class="card-title">DAFTAR MESYUARAT BARU</h5>
		</div>
		<div class="card-body">
			<asp:Panel ID="errorMsg" runat="server" Visible="false">
				<div class="alert alert-warning alert-outline alert-dismissible" role="alert">
					<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
					<h4 class="alert-icon text-danger"><i class="align-middle" data-feather="alert-triangle"></i> <strong>Ralat!</strong></h4>
					<div class="alert-message">
						<asp:BulletedList ID="ErrorList" runat="server" />
					</div>
				</div>
			</asp:Panel>
			<asp:Panel ID="Panel1" runat="server" defaultbutton="btnSubmit">
				<div class="row">
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label">JENIS <span class="text-danger">*</span></label>
						<asp:DropDownList ID="ddlJenis" runat="server" CssClass="form-control form-select" required="required"></asp:DropDownList>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Sila Pilih Jenis Mesyuarat" ControlToValidate="ddlJenis" ForeColor="Red"></asp:RequiredFieldValidator>
					</div>
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label">BILANGAN <span class="text-danger">*</span></label>
						<div class="input-group">
							<asp:TextBox ID="txtBil" type="number" runat="server" CssClass="form-control" placeholder="BIL" min="1" required="required"></asp:TextBox>
							<span class="input-group-text">/</span>
							<asp:TextBox ID="txtTahun" type="number" runat="server" CssClass="form-control" placeholder="TAHUN" min="1900" max="3000" required="required"></asp:TextBox>
						</div>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Sila Isi Bil. Mesyuarat" ControlToValidate="txtBil" ForeColor="Red"></asp:RequiredFieldValidator>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Sila Isi Tahun Bil. Mesyuarat" ControlToValidate="txtTahun" ForeColor="Red" CssClass="float-end"></asp:RequiredFieldValidator>
					</div>
					<div class="col-12 col-lg-6 mb-2">
						<label class="control-label">TARIKH <span class="text-danger">*</span></label>
						<asp:TextBox ID="txtTarikh" type="date" runat="server" CssClass="form-control" placeholder="TARIKH" required="required"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Sila Isi Tarikh Mesyuarat" ControlToValidate="txtTarikh" ForeColor="Red"></asp:RequiredFieldValidator>
					</div>
					<div class="col-12">
						<asp:LinkButton ID="btnSubmit" CssClass="btn btn-primary" runat="server" OnClick="Save">SIMPAN</asp:LinkButton>
					</div>

				</div>
			</asp:Panel>
		</div>
	</div>
	<div class="card">
		<div class="card-header pb-0">
			<h5 class="card-title">CARIAN</h5>
		</div>
		<div class="card-body">
			
			<asp:Panel ID="Panel2" runat="server" defaultbutton="btnSubmit">
				<div class="input-group">
					<asp:DropDownList ID="listSearchCol" CssClass="form-select" runat="server" >  
						<asp:ListItem Value="">SEMUA KOLUM</asp:ListItem> 
						<asp:ListItem>MESYUARAT</asp:ListItem>  
						<asp:ListItem>TARIKH</asp:ListItem>  
						<asp:ListItem>PENGERUSI</asp:ListItem>  
						<asp:ListItem>STATUS PENGESAHAN</asp:ListItem>  
					</asp:DropDownList>
					<asp:TextBox ID="txtSearch" CssClass="form-control w-25" placeholder="Carian..." runat="server"></asp:TextBox>
					<asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server" OnClick="Search" CausesValidation="false"><i class="align-middle" data-feather="search"></i></asp:LinkButton>
				</div>
			</asp:Panel>
		</div>
	</div>
	<div class="card">
		<div class="card-body">
			<div class="row input-group-sm justify-content-between">
				<div class="col-sm-6 col-md-5 mb-3">
					<label>SUSUNAN BERDASARKAN: <asp:Label ID="lblSortRecord" runat="server" >TARIKH &darr;</asp:Label></label>
				</div>
				<div class="col-sm-3 col-md-2 mb-3 text-end">
					
				</div>
			</div>
			<asp:GridView 
				ID="GridView1" 
				runat="server" 
				EmptyDataText="Tiada Rekod Dijumpai." 
				ShowHeaderWhenEmpty="True" 
				AutoGenerateColumns="False" 
				CssClass="table table-bordered table-striped table-hover" 
				OnPageIndexChanging="GridView1_PageIndexChanging" 
				OnRowDataBound="GridView1_OnRowDataBound" 
				OnSorting="GridView1_Sorting" 
				AllowPaging="True" 
				AllowSorting="true"
				PageSize="20">  
			<PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="5" FirstPageText="&laquo;" LastPageText="&raquo;" />
			<PagerStyle HorizontalAlign = "Right" CssClass = "bs-pager" />
            <Columns>
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="MESYUARAT" HeaderText="MESYUARAT" SortExpression="MESYUARAT" ItemStyle-CssClass="text-center text-nowrap" HeaderStyle-CssClass="text-center">
                </asp:BoundField>

				<asp:TemplateField HeaderText="TARIKH" SortExpression="TARIKH" ItemStyle-CssClass="text-center text-nowrap" HeaderStyle-CssClass="text-center">
					<ItemTemplate>
						<%# Eval("TARIKHMS") %>
					</ItemTemplate>
				</asp:TemplateField>

                <asp:BoundField DataField="PENGERUSI" HeaderText="PENGERUSI" SortExpression="PENGERUSI">
                </asp:BoundField>

				<asp:TemplateField HeaderText="KEPUTUSAN/PERMOHONAN" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
					<ItemTemplate>
						<%# Eval("JumlahKelulusan") %>/<%# Eval("JumlahPermohonan") %>
					</ItemTemplate>
				</asp:TemplateField>

				<asp:TemplateField HeaderText="STATUS PENGESAHAN" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
					<ItemTemplate>
						<asp:Label ID="lblStatus" CssClass="badge" runat="server"><%# Eval("StatusPengesahan") %></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>

                <asp:TemplateField ItemStyle-CssClass="text-center">
                    <ItemTemplate>
						<asp:HyperLink ID="viewButton" title="Papar" runat="server"><i class="align-middle" data-feather="eye"></i></asp:HyperLink>
						<asp:HyperLink ID="editButton" CssClass="text-secondary" title="Edit" runat="server"><i class="align-middle" data-feather="edit-2"></i></asp:HyperLink>
						<asp:HyperLink ID="decideButton" CssClass="text-success" title="Keputusan" runat="server"><i class="align-middle" data-feather="inbox"></i></asp:HyperLink>
						<asp:LinkButton 
							ID="lnkDelete" 
							runat="server" 
							CssClass="text-danger"  
							data-bs-toggle="modal" 
							data-bs-target="#deleteModal" 
							title="Hapus"
							CommandArgument='<%# Eval("Id") %>' 
							CausesValidation="false"
							OnClick="BtnDelete_Click"
							OnClientClick='<%# string.Concat("if(!popup(this",",\"MESYUARAT ",Eval("JENIS")," Bil. ",Eval("BILANGAN"),"\"))return false; ") %>'
						>
							<i class="align-middle" data-feather="trash"></i>
						</asp:LinkButton>
					</ItemTemplate>
                </asp:TemplateField>
            </Columns>
			</asp:GridView>
		</div>
	</div>
	<div class="modal fade" id="deleteModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-danger">
					<h5 class="modal-title text-white text-truncate">???</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<p><b class="text-danger">Jika mesyuarat ini dihapuskan, semua permohonan berkaitan akan dikeluarkan daripada mesyuarat ini.</b></p>
					<p class="mb-0">Anda pasti untuk menghapuskan mesyuarat ini?</p>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<a href="#" type="button" class="btn btn-danger">Hapus</a>
				</div>
			</div>
		</div>
	</div>
    <script>   
        function popup(lnk, title) {
            document.querySelector("#deleteModal .modal-header h5").innerText = title;
            document.querySelector("#deleteModal .modal-footer a").setAttribute('href', lnk.getAttribute('href'));

        }
        <%= (Request.QueryString["r"] != null) ? "localStorage.removeItem('open_addMeeting');" : "" %>
        const myCollapsible = document.getElementById('addMeeting');
        myCollapsible.addEventListener('hidden.bs.collapse', event => {
            localStorage.removeItem('open_' + event.target.id);
        })
        myCollapsible.addEventListener('shown.bs.collapse', event => {
            localStorage.setItem('open_' + event.target.id, true);
        })

        document.querySelectorAll(".collapse").forEach(function (elem) {
            // Default close unless saved as open
            if (JSON.parse(localStorage.getItem('open_' + elem.id)) == true) {
                document.getElementById(elem.id).classList.add("show");
			}
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
