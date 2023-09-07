<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="EPBM.errors._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>e-PBM | Ralat</title>
    <style>
        *{
            transition: all 0.6s;
        }

        html {
            height: 100%;
        }

        body{
            font-family: 'Lato', sans-serif;
            color: #888;
            margin: 0;
        }

        #main{
            display: table;
            width: 100%;
            height: 100vh;
            text-align: center;
        }

        .fof{
	          display: table-cell;
	          vertical-align: middle;
        }

        .fof h1{
	          font-size: 50px;
	          display: inline-block;
	          padding-right: 12px;
	          animation: type .5s alternate infinite;
        }

        .fof h3{
	          font-size: 30px;
	          display: inline-block;
	          padding-right: 12px;
        }

        @keyframes type{
	          from{box-shadow: inset -3px 0px 0px #888;}
	          to{box-shadow: inset -3px 0px 0px transparent;}
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main">
    	    <div class="fof">
        		<h1>Harap maaf.</h1>
        		<h3>Ralat ketika memproses laman web!</h3>
    	    </div>
        </div>
    </form>
</body>
</html>
