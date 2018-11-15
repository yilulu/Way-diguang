<script type="text/javascript">
function gets_id(objName){
 if(document.getElementById){
  return eval('document.getElementById("' + objName + '")');
 }else if(document.layers){
  return eval("document.layers['" + objName +"']");
 }else{
  return eval('document.all.' + objName);
 }
}
//´ò¿ªDIV²ã
function disp_cc()
{
 if(gets_id('hh').style.display=='none')
 {
  gets_id('hh').style.display='';
 }
 else
 {
  gets_id('hh').style.display='none';
 }
}
//¸³Öµ
function gets_value(str)
{
 gets_id('class').value=str;
 gets_id('hh').style.display='none';
}
</script>