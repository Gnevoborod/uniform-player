function getListOfComponents(components) {
  let result = '';
  components.forEach(element => {
    result += getComponent(element);
  });
  return result;
}

function getComponent(componentObject) {
  switch (componentObject.type) {
    case 'Label': return getLabel(componentObject);
    case 'Button': return getButton(componentObject);
    case 'TextBox': return getTextBox(componentObject);
    case 'CheckBox': return getCheckBox(componentObject);
    case 'TextArea': return getTextArea(componentObject);
    case 'RadioButton': return getRadioButton(componentObject);
    case 'DateBox': return getDateBox(componentObject);
  }
}

function getComponentForConstructor(componentObject, iterator) {
  let component = new Object();
  component.type = componentObject;
  component.name = 'Name';
  component.label = 'Label';
  component.value = 'Value';
  switch (componentObject) {
    case 'Label': return wrapComponentForAdding(componentObject, 'Метка', getLabel(component), iterator);
    case 'Button': return wrapComponentForAdding(componentObject, 'Кнопка', getButton(component), iterator);
    case 'TextBox': return wrapComponentForAdding(componentObject, 'Текстовое поле ввода', getTextBox(component), iterator);
    case 'CheckBox': return wrapComponentForAdding(componentObject, 'Чекбокс', getCheckBox(component), iterator);
    case 'TextArea': return wrapComponentForAdding(componentObject, 'Многострочное поле ввода', getTextArea(component), iterator);
    case 'RadioButton': return wrapComponentForAdding(componentObject, 'Радиометка', getRadioButton(component), iterator);
    case 'DateBox': return wrapComponentForAdding(componentObject, 'Дата', getDateBox(component), iterator);
  }
}

function getComponentForConstructorForInfo(componentObject) {
  let component = new Object();
  component.type = componentObject;
  component.name = 'Name';
  component.label = 'Label';
  component.value = 'Value';
  switch (componentObject) {
    case 'Label': return wrapComponentForInfo(componentObject, 'Метка', getLabel(component));
    case 'Button': return wrapComponentForInfo(componentObject, 'Кнопка', getButton(component));
    case 'TextBox': return wrapComponentForInfo(componentObject, 'Текстовое поле ввода', getTextBox(component));
    case 'CheckBox': return wrapComponentForInfo(componentObject, 'Чекбокс', getCheckBox(component));
    case 'TextArea': return wrapComponentForInfo(componentObject, 'Многострочное поле ввода', getTextArea(component));
    case 'RadioButton': return wrapComponentForInfo(componentObject, 'Радиометка', getRadioButton(component));
    case 'DateBox': return wrapComponentForInfo(componentObject, 'Дата', getDateBox(component));
  }
}


function wrapComponentForAdding(componentType, name, component, iterator) {
  //showInfo потому как нам showMenu надо вешать на компонент который добавлен на экран. а этот компонент - просто для отображения
  let labelTextStart='';
  let labelTextEnd='';
  let rbchStart='';
  let rbchEnd='';
  switch(componentType){
    case 'Label':labelTextStart = '<div id="labelDemo_'+iterator+'">';
                  labelTextEnd='</div>';  
                  break;
    case 'RadioButton':rbchStart = '<div id="chbRbDemo_'+iterator+'">';
    rbchEnd='</div>';  
                      break;
    case 'CheckBox':rbchStart = '<div id="chbRbDemo_'+iterator+'">';
    rbchEnd='</div>';  
                    break;
  }
  return rbchStart+'<div id=' + componentType + '_' + iterator + ' class="tsz14 dottedBorder topBottomGap leftGap rightGap" ondblclick="deleteFromScreen(' + iterator + ')" onclick="showMenu(\'component\',\'' + componentType + '\',\'' + iterator + '\')"><p style="height:2px;text-align:center"><b>' + name + '</b></p>'+labelTextStart + component + labelTextEnd + '</div>'+rbchEnd;
}

function wrapComponentForInfo(componentType, name, component) {
  return '<div id=' + componentType + ' style="width:80%;" class="tsz14 dottedBorder topBottomGap leftGap rightGap" ondblclick="addToScreen(\'' + componentType + '\')" onclick="showInfo(\'component\',\'' + componentType + '\')"><p style="height:2px;text-align:center"><b>' + name + '</b></p>' + component + '</div>';
}
function getLabel(label) {
  return '<p name="' + label.name + '" class="uniform-player-component topBottomGap leftGap">' + label.value + '</p>';
}

function getButton(btn) {
  hasButton = true;
  return '<button class="uniform-player-component tsz14 btn btn-secondary topBottomGap leftGap" name="' + btn.name + '" onclick = getNextScreen(this) value="' + btn.value + '">' + btn.value + '</button>';
}

function getTextBox(textbox) {
  var placeholder = textbox.properties === undefined ? '' : textbox.properties;

  return '<div class="form-group topBottomGap" style="text-align: left;"><label style="margin:0 auto;margin-left:3%" class="col-form-label leftGap" for="' + textbox.name + '">' + textbox.label + '</label><input type="text" style="width:48%; margin-left:2%" class="uniform-player-component form-control form-control-sm" placeholder="' + placeholder + '" id="' + textbox.name + '" name="' + textbox.name + '" value="' + textbox.value + '"></div>';
}

function getCheckBox(checkbox) {
  var checked = (checkbox.value == null || checkbox.value == 0) ? ' ' : "checked";
  return '<div class="form-check" style="text-align: left;"><input class="uniform-player-component form-check-input topBottomGap leftGap" style="margin:0 auto;margin-top:8px;" type="checkbox" value="' + checkbox.value + '" id="' + checkbox.name + '" onclick="switchValue(this)" ' + checked + ' name="' + checkbox.name + '"><label class="form-check-label" style="margin:0 auto;margin-top:4px;margin-left:4px;" for="' + checkbox.name + '">' + checkbox.label + '</label></div>';
}

function switchValue(obj) {

  if (obj.value == null || obj.value == 0)
    obj.value = 1;
  else
    obj.value = 0;
}

function getTextArea(textarea) {
  return '<div class="form-group topBottomGap" style="text-align: left;"><label for="' + textarea.name + '" style="margin:0 auto;margin-left:3%" class="form-label topBottomGap leftGap">' + textarea.label + '</label><textarea style="width:96%; margin-left:2%" name="' + textarea.name + '" value="' + textarea.value + '" class="tsz14 uniform-player-component form-control" id="' + textarea.name + '" rows="1">' + textarea.value + '</textarea></div>';
}

function getRadioButton(radio) {
  var checked = (radio.value == null || radio.value == 0) ? ' ' : "checked";
  return '<div class="form-check topBottomGap" style="text-align: left;"><input class="uniform-player-component  form-check-input topBottomGap leftGap"  style="margin:0 auto; margin-top:8px;" type="radio" name="' + radio.name + '" id="' + radio.name + '" onclick="switchValue(this)" value="' + radio.value + '" ' + checked + '><label style="margin-top:4px;margin-left:4px;" class="form-check-label" for="' + radio.name + '">' + radio.label + '</label></div>';
}

function getDateBox(date) {
  return '<div class="form-group topBottomGap" style="text-align: left;"><label style="margin:0 auto;margin-left:3%" class="col-form-label topBottomGap leftGap" for="' + date.name + '">' + date.label + '</label><input type="date" class="uniform-player-component form-control-sm form-control" style="width:48%; margin:0 auto; margin-left:2%;" id="' + date.name + '" name="' + date.name + '" value="' + date.value + '"></div>';
}

function showInfo(component, cmpType) {
  switch (cmpType) {
    case 'Label': return 'Многострочное поле для отображения текста, поддерживающее разметку.';
    case 'Button': return 'Кнопка для переходов между страницами.';
    case 'TextBox': return 'Однострочное поле ввода (имя, адрес, почта).';
    case 'CheckBox': return 'Чекбокс.';
    case 'TextArea': return 'Многострочное поле ввода.';
    case 'RadioButton': return 'Радиокнопка, для формирования радиогрупп.';
    case 'DateBox': return 'Поле ввода даты.';
  }
}

function getNewScreenButton() {
  return '<div style="width:50%; float:left;"><button class="uniform-player-screen tsz14 btn btn-secondary topBottomGap leftGap" onclick="showNewScreen()">Новый экран</button></div>';
}

function getPreviewButton() {
  return '<div style="float:right;"><button class="uniform-player-screen tsz14 btn btn-secondary topBottomGap rightGap" onclick="showPreview()">Предпросмотр</button></div>';
}

function showSaveButton() {
  return document.getElementById("saveButtonContainer").innerHTML = '<button class="uniform-player-screen tsz14 btn btn-secondary topBottomGap leftGap" onclick="saveScreenToList()">Сохранить экран</button>';
}

function showSaveScenario() {
  return document.getElementById("saveScenarioButtonContainer").innerHTML = '<button class="uniform-player-screen tsz14 btn btn-secondary topBottomGap leftGap" onclick="saveScenario()">Сохранить сценарий</button>';
}

function showPublishButton()
{
   return document.getElementById("publishScenarioButtonContainer").innerHTML = '<button class="uniform-player-screen tsz14 btn btn-secondary topBottomGap leftGap" onclick="publishScenario()">Опубликовать сценарий</button>';
}

function showGetDcenarioButton()
{
   return document.getElementById("getScenario").innerHTML = '<button class="uniform-player-screen tsz14 btn btn-secondary topBottomGap leftGap" onclick="loadScenario()">Загрузить сценарий</button>';
}


function addTransitionRulesButton()
{
   return document.getElementById("transitions").innerHTML = '<button style="height:35px;" class="uniform-player-screen tsz14 btn btn-secondary topBottomGap leftGap" onclick="addNewTransition()">Добавить переход</button>';
}

