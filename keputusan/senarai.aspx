<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="senarai.aspx.cs" Inherits="EPBM.keputusan.senarai" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<h1 class="h3 mb-3">SENARAI <strong>KEPUTUSAN</strong></h1>
	<div class="card d-none">
		<div class="card-body">
			<asp:Panel ID="Panel1" runat="server">
				<div class="input-group">
					<asp:DropDownList ID="listSearchCol" CssClass="form-select" runat="server" >  
						<asp:ListItem Value="">SEMUA KOLUM</asp:ListItem>  
						<asp:ListItem>MESYUARAT</asp:ListItem>  
						<asp:ListItem>TAJUK</asp:ListItem>  
						<asp:ListItem>JABATAN</asp:ListItem>  
						<asp:ListItem>STATUS</asp:ListItem>  
						<asp:ListItem>SYARIKAT BERJAYA</asp:ListItem>  
					</asp:DropDownList>
					<asp:TextBox ID="txtSearch" CssClass="form-control w-25" placeholder="Carian..." runat="server"></asp:TextBox>
					<asp:LinkButton ID="btnSubmit" CssClass="btn btn-primary" runat="server" OnClick="Search" CausesValidation="false"><i class="align-middle" data-feather="search"></i></asp:LinkButton>
					<a class="btn btn-secondary ms-3 rounded" data-bs-toggle="collapse" href="#advancedSearch" role="button" aria-expanded="false" aria-controls="advancedSearch"><i class="align-middle" data-feather="filter"></i></a>
				</div>
			</asp:Panel>
		</div>
	</div>
	<div id="advancedSearch" class="card">
		<div class="card-header pb-0">
			<h5 class="card-title">CARIAN</h5>
		</div>
		<div class="row card-body">
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">MESYUARAT</label>
					<asp:DropDownList ID="listMesyuarat" CssClass="form-select" runat="server" />
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">TAJUK</label>
					<asp:TextBox ID="txtTajuk" runat="server" CssClass="form-control"  />
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">JABATAN</label>
					<div class="input-group mb-3 other-input">
						<asp:DropDownList ID="listJabatan" CssClass="form-select" runat="server" />
						<asp:DropDownList ID="listBahagian" CssClass="form-select w-50 d-none" runat="server" />
					</div>
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">STATUS</label>
					<asp:TextBox ID="txtStatus" runat="server" CssClass="form-control"  />
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">SYARIKAT BERJAYA</label>
					<div class="input-group">
						<asp:DropDownList ID="listCondSyarikat" CssClass="form-select" runat="server" >  
							<asp:ListItem>MENGANDUNGI</asp:ListItem>  
							<asp:ListItem>SAMA DENGAN</asp:ListItem>  
							<asp:ListItem>BERMULA DENGAN</asp:ListItem>  
							<asp:ListItem>BERAKHIR DENGAN</asp:ListItem>  
						</asp:DropDownList>
						<asp:TextBox ID="txtSyarikat" runat="server" CssClass="form-control w-50"  />
					</div>
				</div>
				<div class="col-12 text-end">
					<asp:HyperLink ID="btnReset" NavigateUrl="/keputusan/senarai.aspx" runat="server" CssClass="btn btn-outline-danger" >RESET</asp:HyperLink>
					<asp:LinkButton ID="btnSubmit2" CssClass="btn btn-primary" runat="server" OnClick="SearchExtend" PostBackUrl="~/keputusan/senarai.aspx" CausesValidation="false">CARI</asp:LinkButton>
				</div>
		</div>
	</div>
	<div class="card">
		<div class="card-body table-responsive">
			<div class="row input-group-sm justify-content-between">
				<div class="col-sm-6 col-md-5 mb-3">
					<label>SUSUNAN BERDASARKAN: <asp:Label ID="lblSortRecord" runat="server" >ID &darr;</asp:Label></label>
				</div>
				<div class="col-sm-3 col-md-2 mb-3 text-end">
					<asp:LinkButton ID="btnCetak" CssClass="btn btn-info btn-sm" runat="server" CommandArgument="basic" PostBackUrl="~/keputusan/cetak-senarai.aspx" ><i class="align-middle" data-feather="printer"></i> Cetak</asp:LinkButton>
				</div>
			</div>
			<asp:GridView 
				ID="GridView1" 
				runat="server" 
				EmptyDataText="Tiada Rekod Dijumpai." 
				ShowHeaderWhenEmpty="True" 
				AutoGenerateColumns="False" 
				OnRowDataBound="GridView1_OnRowDataBound" 
				OnSorting="GridView1_Sorting" 
				OnPageIndexChanging="GridView1_PageIndexChanging" 
				AllowSorting="true"
				AllowCustomPaging="true"
				AllowPaging="True" 
				PageSize="15"
				CssClass="table table-bordered table-striped table-hover">  

				<PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="5" FirstPageText="&laquo;" LastPageText="&raquo;" />
				<PagerStyle HorizontalAlign = "Right" CssClass = "bs-pager" />

				<Columns>
					<asp:TemplateField HeaderText="#">
						<ItemTemplate><asp:Literal ID="Numbering" runat="server" /></ItemTemplate>
					</asp:TemplateField>

					<asp:BoundField DataField="MESYUARAT" HeaderText="MESYUARAT" SortExpression="MESYUARAT">
					</asp:BoundField>

					<asp:BoundField DataField="TAJUK" HeaderText="TAJUK" SortExpression="TAJUK">
					</asp:BoundField>

					<asp:BoundField DataField="JABATAN" HeaderText="JABATAN" HeaderStyle-CssClass="text-center" SortExpression="JABATAN">
					</asp:BoundField>

					<asp:BoundField DataField="JENIS PERTIMBANGAN" HeaderText="JENIS PERTIMBANGAN" HeaderStyle-CssClass="text-center" SortExpression="JENIS PERTIMBANGAN">
					</asp:BoundField>

					<asp:BoundField DataField="MUKTAMAD" HeaderText="MUKTAMAD" HeaderStyle-CssClass="text-center" SortExpression="MUKTAMAD">
					</asp:BoundField>

					<asp:TemplateField HeaderText="STATUS" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" SortExpression="STATUS">
						<ItemTemplate>
							<asp:Label ID="lblStatus" CssClass="badge" runat="server"><%# Eval("STATUS") %></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>

					<asp:TemplateField HeaderText="KETERANGAN" HeaderStyle-CssClass="text-center">
						<ItemTemplate>
							<asp:Label ID="LblKeterangan" runat="server"><%# Eval("KETERANGAN").ToString().Replace(Environment.NewLine, "<br />") %></asp:Label>
						
							<asp:ListView runat="server"  ID="DetailsList">
								<LayoutTemplate>
									<ul class="list-group text-sm">
										<li id="itemPlaceholder" runat="server" />
									</ul>
								</LayoutTemplate>
								<ItemTemplate>
									<li class="list-group-item p-1"><b><%#: Eval("Label") %>:</b> <%#: Eval("Text") %></li>
								</ItemTemplate>
							</asp:ListView>
						</ItemTemplate>
					</asp:TemplateField>

					<asp:TemplateField ItemStyle-CssClass="text-center">
						<ItemTemplate>
							<a href="/keputusan/papar.aspx?id=<%# Eval("Id") %>&ReturnURL=<%# System.Web.HttpUtility.UrlEncode("/keputusan/senarai.aspx") %>" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
							<asp:HyperLink ID="LinkEditSST" runat="server" CssClass="text-secondary" title="Kemaskini SST" Visible="false" ><i class="align-middle" data-feather="edit-2"></i></asp:HyperLink>
							<asp:HyperLink ID="LinkEditMOF" runat="server" CssClass="text-secondary" title="Keputusan MOF" Visible="false" ><i class="align-middle" data-feather="edit"></i></asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</div>
	</div>
	
	<script>
        var otherInput = document.getElementById('<%=listJabatan.ClientID %>');
	
		otherInput.addEventListener('change', function (event) {
			if(otherInput.value=="1"){
				otherInput.nextElementSibling.required=true;
				otherInput.nextElementSibling.classList.remove('d-none');
			}
			else{
				otherInput.nextElementSibling.required=false;
				otherInput.nextElementSibling.classList.add('d-none');
			}
		});
		otherInput.dispatchEvent(new Event('change', { 'bubbles': true }));

		/*const myCollapsible = document.getElementById('advancedSearch');
        myCollapsible.addEventListener('hidden.bs.collapse', event => {
            localStorage.removeItem('open_' + event.target.id);
            document.getElementById("<%=btnSubmit.ClientID %>").classList.remove("d-none");
        })
        myCollapsible.addEventListener('shown.bs.collapse', event => {
            localStorage.setItem('open_' + event.target.id, true);
            document.getElementById("<%=btnSubmit.ClientID %>").classList.add("d-none");
        })

        document.querySelectorAll(".collapse").forEach(function (elem) {
            // Default close unless saved as open
			if (JSON.parse(localStorage.getItem('open_' + elem.id)) == true) {
				document.getElementById(elem.id).classList.add("show");
                document.getElementById("<%=btnSubmit.ClientID %>").classList.add("d-none");
			}
            else
                document.getElementById("<%=btnSubmit.ClientID %>").classList.remove("d-none");
        });*/
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
