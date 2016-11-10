/////////////////////////////////////////////////////////////////////////////////
//Code Module  : JScriptOne
//Project      : PSHR Search Engine
//Author       : Santosh Kumar
//Role         : Project Leader
//Designation  : Project Leader
//Department   : Software Engineering
//Created Date : 23-Nov-2007
//Modified by  : 
//Modified on  : 
//
//Description  : This contains global methods required for client side executions
////////////////////////////////////////////////////////////////////////////////

function doStop(){
		history.forward(1);
		//status="Please don't use the forward/back buttons";
}

document.onload = doStop();
