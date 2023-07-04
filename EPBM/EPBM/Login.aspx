<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EPBM.Login" %>


<!DOCTYPE html>

<html lang="en">
<head>
  <meta charset="utf-8">
  <meta content="width=device-width, initial-scale=1.0" name="viewport">

  <title>Components / Accordion - NiceAdmin Bootstrap Template</title>
  <meta content="" name="description">
  <meta content="" name="keywords">

  <!-- Favicons -->
  <link href="assets/img/favicon.png" rel="icon">
  <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">

  <!-- Google Fonts -->
  <link href="https://fonts.gstatic.com" rel="preconnect">
  <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Nunito:300,300i,400,400i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

  <!-- Vendor CSS Files -->
  <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <link href="assets/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet">
  <link href="assets/vendor/boxicons/css/boxicons.min.css" rel="stylesheet">
  <link href="assets/vendor/quill/quill.snow.css" rel="stylesheet">
  <link href="assets/vendor/quill/quill.bubble.css" rel="stylesheet">
  <link href="assets/vendor/remixicon/remixicon.css" rel="stylesheet">
  <link href="assets/vendor/simple-datatables/style.css" rel="stylesheet">

  <!-- Template Main CSS File -->
  <link href="assets/css/style.css" rel="stylesheet">

  <!-- =======================================================
  * Template Name: NiceAdmin - v2.2.2
  * Template URL: https://bootstrapmade.com/nice-admin-bootstrap-admin-html-template/
  * Author: BootstrapMade.com
  * License: https://bootstrapmade.com/license/
  ======================================================== -->
<style>

    .login,
.image {
  min-height: 100vh;
}

.bg-image {
  background-image: url('https://bootstrapious.com/i/snippets/sn-page-split/bg.jpg');
  background-size: cover;
  background-position: center center;
}
</style>



</head>
<body>
<div class="container-fluid">
    <div class="row no-gutter">
        <!-- The image half -->
        <div class="col-md-6 d-none d-md-flex bg-image"></div>


        <!-- The content half -->
        <div class="col-md-6 bg-light">
            <div class="login d-flex align-items-center py-5">

                <!-- Demo content-->
                   
                <div class="container">
<form id="form2" runat="server" defaultbutton="BtnLogin" class="login-form">   
                    <div class="row"> 
                        <div class="col-lg-10 col-xl-7 mx-auto">
                            <h3 class="display-4">Split page!</h3>
                            <p class="text-muted mb-4">Create a login split page using Bootstrap 4.</p>
                                                        
                                <div class="form-group mb-3">
                                     <asp:TextBox ID="txtUsername" runat="server" class="form-control rounded-pill border-0 shadow-sm px-4" autofocus="autofocus" placeholder="No Kad Pengenalan" OnTextChanged="txtUsername_TextChanged"></asp:TextBox>
<%--                                    <input id="inputEmail" type="email" placeholder="Email address" required="" autofocus="" class="form-control rounded-pill border-0 shadow-sm px-4">--%>
                                </div>
                                <div class="form-group mb-3">
                                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" class="form-control rounded-pill border-0 shadow-sm px-4 text-primary" autofocus="autofocus" placeholder="Kata Laluan"></asp:TextBox>
  <%--                                  <input id="inputPassword" type="password" placeholder="Password" required="" class="form-control rounded-pill border-0 shadow-sm px-4 text-primary">--%>
                                </div>
                                <div class="custom-control custom-checkbox mb-3">
                                   <%-- <input id="customCheck1" type="checkbox" checked class="custom-control-input">--%>
                                    <%--<label for="customCheck1" class="custom-control-label">Remember password</label>--%>
                                </div>
                                 <asp:LinkButton ID="BtnLogin" runat="server" CssClass="width-30 pull-right btn btn-sm btn-primary" OnClick="Login_Click"><span class="ace-icon fa fa-key"></span> <span class="bigger-110">Login</span> </asp:LinkButton>
                                                    </div>

                        </div>
                                <%--<div class="text-center d-flex justify-content-between mt-4"><p>Snippet by <a href="https://bootstrapious.com/snippets" class="font-italic text-muted"> 
                                        <u>Boostrapious</u></a></p></div>--%>
                             <!--<asp:CheckBox ID="chkPersist" runat="server" Text="Persist Cookie" /> -->
                                <asp:Label ID="errorLabel" style="width:100%;" runat="server" class="label label-danger"></asp:Label>
                          </form>
                        </div>
                    </div>
                </div><!-- End -->

            </div>
        </div><!-- End -->

<%--    </div>
</div>--%>
</body>
</html>


