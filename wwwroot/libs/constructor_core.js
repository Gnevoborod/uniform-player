//place of global variables definitions
let screenList = new Array();

let transition = null;


let rules = null;

let currentScreenObject = null;
let currentComponent = null;
let currentRule = null;

let firstScreen = '';
let firstcScreenChbChecked = false;
let rnd = 0;

let transForChoose = -1;
let ruleForChoose = -1;

let tempScreenNum=0;

let rulesPseudo = new Array();

function makeNewScreen() {
  currentScreenObject = new Object();
  currentScreenObject.name = '';
  currentScreenObject.type = 'Common';
  currentScreenObject.title = '';
  currentScreenObject.components = new Array();
  currentScreenObject.description = '';
  currentScreenObject.transitions = new Array();
  //screenList.push(currentScreenObject);
  return currentScreenObject;
}

function makeNewComponent(componentType) {
  currentComponent = new Object();
  currentComponent.name = '';
  currentComponent.description = '';
  currentComponent.type = componentType;
  currentComponent.value = '';
  currentComponent.label = '';
  currentComponent.properties = '';

}


function makeTransition(screen) {
  transition = new Object();
  transition.rules = new Array();
  transition.nextScreen = screen;
}

function makeRule(cntId) {
  rule = new Object();
  rule.containerId = cntId;
  rule.description = '';
  rule.componentName = '';
  rule.predicate = '';
  rule.value = '';
  return rule;
}

function addNewRuleToTransition() {
  transition.rules.push(rule);
}

function addTransitionToScreen() {
  console.log("screen is "+currentScreenObject.name);
  console.log("transitions are "+currentScreenObject.transitions.length);
  currentScreenObject.transitions.push(transition);
}

function getScreenData(screenName) {
  screenList.forEach(element => {
    if (element.name == screenName)
      return element;
  });

}

function saveScreenToList() {
  if(currentScreenObject.name==='')
  currentScreenObject.name='temp'+(tempScreenNum++);
  if (screenList.length === 0) {
    screenList.push(currentScreenObject);
    showScreensAtContainer();
    return true;
  }
  var len = screenList.length;
  for (var i = 0; i < len; i++) {
    if (screenList[i].name !== currentScreenObject.name) {
      if (CheckIfExists())
        screenList.push(currentScreenObject);

      showScreensAtContainer();
      return true;
    }
  }
}


function CheckIfExists() {
  var len = screenList.length;
  for (var i = 0; i < len; i++) {
    if (screenList[i].name === currentScreenObject.name) {
      return false;
    }
  }
  return true;
}

function updateScreenName(newValue) {
  if (firstcScreenChbChecked && firstScreen === currentScreenObject.name)
    firstScreen = newValue;
  currentScreenObject.name = newValue;

}

function updateScreenTitle(newTitle) {
  currentScreenObject.title = newTitle;
  document.getElementById("title").innerHTML = '<h5 class="card-title translucent">' + newTitle + '</h5>';
}

function updateDescriptionTitle(newDescr) {
  currentScreenObject.description = newDescr;
}

function setFirstScreen() {
  let args = document.getElementById("screenNameConstructor").value;
  firstcScreenChbChecked = !firstcScreenChbChecked;
  if (firstScreen === args) {
    firstScreen = '';
  }
  else {
    firstScreen = args;
  }
}

function showNewScreen(element) {
  if (document.getElementById("transitions").hidden)
    document.getElementById("transitions").hidden = false;
  let screenObject = makeNewScreen();
  firstcScreenChbChecked = false;
  let body = '<div id="specificScreen"><div class="card-body dottedBorder topBottomGap" style="text-align: center; ">\
              <div class="card-title dottedBorder" onclick="showMenu(\'screen\', this)" id="title"\
               style="margin-top: 4px; margin-bottom: 4px;height: 10%;width:100%;overflow-y:auto;margin:0 auto;">\
                <h5 class="card-title translucent">\
                  Заголовок экрана</h5>\
              </div>\
              <div class="card-text dottedBorder" id="screenBody"\
                style="margin-top: 5px; margin-bottom: 5px;height: 60%; width:auto;overflow-y:auto;margin:0 auto; align-items: center;">\
                <h5 class="card-title translucent">\
                  Компоненты экрана</h5>\
              </div>\
            </div>\
            <div id="controls" class="card-text dottedBorder"\
              style=" text-align: center; margin-top: 4px; margin-bottom: 4px;height:5%;width:100%;overflow-y:auto;margin:0 auto;">\
              <h6 class="card-title translucent">\
                Кнопка появится автоматически, если не будет кнопок-компонент</h6>\
            </div>'+ getNewScreenButton() + getPreviewButton() + '</div>';
  document.getElementById("prototype").innerHTML = body;
  refreshScreenSettings();
  showSaveButton();
  showSaveScenario();
  showPublishButton();
  showGetDcenarioButton();
  addTransitionRulesButton();
  saveScreenToList()
}

function showScreen(identity) {
  if (identity != null)
    currentScreenObject = screenList[identity];
  firstcScreenChbChecked = currentScreenObject.name === firstScreen ? true : false;
  let body = '<div id="specificScreen"><div class="card-body dottedBorder topBottomGap" style="text-align: center; ">\
              <div class="card-title dottedBorder" onclick="showMenu(\'screen\', this)" id="title"\
               style="margin-top: 4px; margin-bottom: 4px;height: 10%;width:100%;overflow-y:auto;margin:0 auto;">\
                <h5 class="card-title translucent">\
                  '+ currentScreenObject.title + '</h5>\
              </div>\
              <div class="card-text dottedBorder" id="screenBody"\
                style="margin-top: 5px; margin-bottom: 5px;height: 60%; width:auto;overflow-y:auto;margin:0 auto; align-items: center;">\
                '+ showExistedComponentsOnExistedScreen() + '\
              </div>\
            </div>\
            <div id="controls" class="card-text dottedBorder"\
              style=" text-align: center; margin-top: 4px; margin-bottom: 4px;height:5%;width:100%;overflow-y:auto;margin:0 auto;">\
              <h6 class="card-title translucent">\
                Кнопка появится автоматически, если не будет кнопок-компонент</h6>\
            </div>'+ getNewScreenButton() + getPreviewButton() + '</div>';
  document.getElementById("prototype").innerHTML = body;
  refreshScreenSettings();
  refreshRuleArea();
}

function refreshRuleArea() {
  addTransitionRulesButton();
  showAllTransitions();
}

function refreshScreenSettings() {
  document.getElementById("settingsBlock").innerHTML = showScreenSettings();
}

function showScreenSettings() {
  cleanSettingsBody();
  var checked = ' ';
  if (firstScreen !== '' && firstScreen === currentScreenObject.name)
    checked = "checked";
  return '<div class="form-group  topBottomGap leftGap rightGap"><label class="col-form-label col-form-label-sm " for="screenNameConstructor">Идентификатор\
        экрана</label><input class="form-control form-control-sm" placeholder="Например: first_screen" type="text"\
        value="'+ currentScreenObject.name + '" id="screenNameConstructor" onchange="updateScreenName(this.value)">\
        <label class="col-form-label col-form-label-sm " for="screenTitleConstructor">Заголовок\
        экрана</label><input class="form-control form-control-sm" placeholder="Например: Самый первый экран" type="text"\
        value="'+ currentScreenObject.title + '" id="screenTitleConstructor" onchange="updateScreenTitle(this.value)"></input>\
        <label class="col-form-label col-form-label-sm " for="screenDescriptionConstructor">Описание\
        экрана</label><input class="form-control form-control-sm" placeholder="Например: Экран для указания контактных данных" type="text"\
        value="'+ currentScreenObject.description + '" id="screenDescriptionConstructor" onchange="updateDescriptionTitle(this.value)"></input>\
        <div class="form-check" style="text-align: left;"><input class="form-check-input topBottomGap leftGap" style="margin:0 auto;margin-top:8px;" type="checkbox"\
        value="" '+ checked + ' id="firstScreenChb" name="" onclick="setFirstScreen(' + checked + ')"><label class="form-check-label" style="margin:0 auto;margin-top:4px;margin-left:4px;" for="firstScreenChb">Первый экран сценария</label></div></div>';
}



function showComponent(typeOfObj, identity) {
  cleanSettingsBody();
  if (identity !== null)
    currentComponent = currentScreenObject.components[identity];
  let val = '';
  if (typeOfObj == 'Label') {
    val = '<label class="col-form-label col-form-label-sm " for="cmpValueConstructor">Значение\
    компонента по-умолчанию</label><textarea style="width:96%; margin-left:2%"\
     value="' + currentComponent.value + '" class="uniform-player-component tsz14 form-control" placeholder="Например описание какого-либо процесса или требований" rows="5"  onchange="updateCmpValue(this.value,\'' + identity + '\')">' + currentComponent.value + '</textarea>';

  }
  else {
    val = '<label class="col-form-label col-form-label-sm " for="cmpValueConstructor">Значение\
    компонента по-умолчанию</label><input class="form-control form-control-sm" placeholder="Например: 2023" type="text"\
    value="'+ currentComponent.value + '" id="cmpValueConstructor" onchange="updateCmpValue(this.value,\'' + identity + '\')"></input>';
  }

  let chkradLabel = '';
  if (typeOfObj == 'RadioButton' || typeOfObj == 'CheckBox') {
    chkradLabel = '<label class="col-form-label col-form-label-sm " for="cmpLabelConstructor">Подпись\
    компонента выбора</label><input class="form-control form-control-sm" placeholder="Например: Согласен" type="text"\
    value="'+ currentComponent.label + '" id="cmpLabelConstructor" onchange="updateCmpLabel(this.value,\'' + identity + '\')"></input>';
  }
  return '<div class="form-group topBottomGap leftGap rightGap"><label class="col-form-label col-form-label-sm" for="componentNameConstructor">Идентификатор компонента</label>\
  <input class="form-control form-control-sm" placeholder="Например: text_box_1" type="text" value="'+ currentComponent.name + '" id="componentNameConstructor" onchange="updateCmpName(this.value)">\
  <label class="col-form-label col-form-label-sm " for="cmpDescriptionConstructor">Описание\
        компонента</label><input class="form-control form-control-sm" placeholder="Например: Компонент выбора даты" type="text"\
        value="'+ currentComponent.description + '" id="cmpDescriptionConstructor" onchange="updateCmpDescription(this.value)"></input>'
    + val + chkradLabel +
    '<label class="col-form-label col-form-label-sm " for="cmpPropConstructor">Дополнительные\
        правила</label><input class="form-control form-control-sm" placeholder="Например текст для размещения в плейсхолдере" type="text"\
        value="'+ currentComponent.properties + '" id="cmpPropConstructor" onchange="updateCmpProperties(this.value)"></input>\
  </div>';
}


function updateCmpName(newName) {
  currentComponent.name = newName;

}

function updateCmpDescription(descr) {
  currentComponent.description = descr;
}

function updateCmpValue(val, identity) {
  currentComponent.value = val;
  if (currentComponent.type == 'Label')
    document.getElementById("labelDemo_" + identity).innerHTML = val;
}

function updateCmpProperties(props) {
  currentComponent.properties = props;
}

function updateCmpLabel(label, identity) {
  currentComponent.label = label;
  if (currentComponent.type == 'RadioButton' || currentComponent.type == 'CheckBox') {
    let component = new Object();
    component.type = currentComponent.type;
    component.name = currentComponent.name;
    component.label = currentComponent.label;
    component.value = currentComponent.value;
    let lbl = currentComponent.type == 'RadioButton' ? 'Радиометка' : 'Чекбокс';
    document.getElementById("chbRbDemo_" + identity).innerHTML = wrapComponentForAdding(currentComponent.label, lbl, getCheckBox(component), identity);
  }
}
function addToScreen(componentType) {
  if (currentScreenObject == null)
    return;
  makeNewComponent(componentType);

  currentScreenObject.components.push(currentComponent);
  //componentList.push(currentComponent);
  //componentsToScreens.push(currentScreenObject.components.length-1);
  //saveComponentToList();
  document.getElementById("screenBody").innerHTML += getComponentForConstructor(componentType, currentScreenObject.components.length - 1);
}

function deleteFromScreen(identity) {
  //var identityToDelete=componentsToScreens[identity];
  currentScreenObject.components.splice(identity, 1);
  //componentList.splice(identity,1);
  //componentsToScreens.splice(identity,1);
  showScreen(null);
}

function showScreensAtContainer() {
  var amount = screenList.length;
  let container = document.getElementById("screenListContainer");
  container.innerHTML = '';
  for (let i = 0; i < amount; i++) {
    container.innerHTML += '<div id="screenImage_' + i + '" class="leftGap style="float:left; height:" class="dottedBorder" onclick="showScreen(' + i + ')"><img src="../images/screen.png"><p style="text-align:center;">' + screenList[i].name + '</p></div>';
  }
}

function showExistedComponentsOnExistedScreen(screenId) {
  var len = currentScreenObject.components.length;

  var result = '';
  for (var i = 0; i < len; i++) {
    result += getComponentForConstructor(currentScreenObject.components[i].type, i);

  }
  return result;
}

function showPreview() {



}


function saveScenario() {

  xmlhttp.onreadystatechange = function () {
    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
      alert('ok');
    }
    else {
      document.body.append = "No answer";

    }
  };
  var firstScreenV = { firstScreen: firstScreen };

  var scrList = new Array();
  for (var i = 0; i < screenList.length; i++) {
    var newScr = new Object();
    newScr.name = screenList[i].name;
    newScr.type = screenList[i].type;
    newScr.title = screenList[i].title;
    newScr.components = screenList[i].components;
    newScr.description = screenList[i].description;
    newScr.transitions = new Array();
    for (var j = 0; j < screenList[i].transitions.length; j++) {
      newTransition = new Object();
      newTransition.rules = new Array();
      newTransition.nextScreen = screenList[i].transitions[j].nextScreen;
      for (var k = 0; k < screenList[i].transitions[j].rules.length; k++) {
        var newRule = new Object();
        newRule.description = screenList[i].transitions[j].rules[k].description;
        newRule.componentName = screenList[i].transitions[j].rules[k].componentName;
        newRule.predicate = screenList[i].transitions[j].rules[k].predicate;
        newRule.value = screenList[i].transitions[j].rules[k].value;
        newTransition.rules.push(newRule);
      }
      newScr.transitions.push(newTransition);
    }
    scrList.push(newScr);
  }
  var full = { configuration: firstScreenV, screens: scrList };
  var request = JSON.stringify(full);
  console.log(request);
  var scenarioId = document.getElementById("scenarioId").value;
  if (scenarioId.length < 1) {
    document.getElementById("scenarioId").style = "color:red;";
    return;
  }
  xmlhttp.open('POST', '../scenarios/upload/' + scenarioId, true);
  xmlhttp.setRequestHeader("Content-type", "application/json");
  xmlhttp.send(request);
}


function deleteRule(transI, ruleI) {
  currentScreenObject.transitions[transI].rules.splice(ruleI, 1);
  refreshRuleArea();
}

function deleteTransition(transI) {
  currentScreenObject.transitions.splice(transI, 1);
}


function addNewTransition() {

  let transitionArea = document.getElementById("transitions");
  let local = currentScreenObject.transitions.length;//rnd;
  //rnd++;
  last = 0;
  transitionArea.innerHTML += '<div class="under dottedBorder" onclick="loadCurrentRule(\'' + local + '\')" style="position:relative;width:100%">\
  <div class="under rightGap dottedBorder" style="width:100%;"><div class="form-group" style="text-align:left;width:100%;"><label class="col-form-label col-form-label-sm tsz14 leftGap" for="screenDestination">Экран назначения</label>\
  <input type="text" class="form-control form-control-sm topBottomGap leftGap tsz14 transitionWidthS transitionHeight" onchange="updateTransitionScreen(this.value)" id="screenDestination'+ local + '" placeholder="Сюда наверное стоит воткнуть выпадашку с экранами">\
  <div id="rulesContainer'+ local + '" class="under dottedBorder" style="width:100%;"></div></div>\
  <div class="under rightGap" style="height:100%;"><button style="height:35px;width:148px;" onclick="addNewRule(this.value)" value="'+ local + '" class="tsz14 btn btn-info topBottomGap leftGap">Добавить правило</button></div></div></div>';
  var scrDest = document.getElementById("screenDestination" + local);
  makeTransition(scrDest.value);
  addTransitionToScreen();
}



function addNewRule(_local) {
  let local = 0;
  console.log("_local is "+_local);
  local = currentScreenObject.transitions[_local].rules.length;

  //local=rulesPseudo.length;

  currentRule = makeRule(local);
  rulesPseudo.push(currentRule);
  addNewRuleToTransition();
  console.log(_local);

  document.getElementById("rulesContainer" + _local).innerHTML += '<details onclick="setRuleForChoose(\'' + local + '\')" style="height:80%;width:100%;text-align:left;" id="' + local + '"><summary style="class="width:100%;word-spacing:80%;">Правило\
  <label class="col-form-label col-form-label-sm tsz14 leftGap" onclick="deleteRule(\''+ _local + '\',\'' + local + '\')">X</label></summary>\
  <div class="under rightGap">\
  <label class="col-form-label col-form-label-sm tsz14 leftGap" for="componentSource">Компонент</label>\
  <input type="text" value="'+ currentRule.componentName + '" onchange="updateRuleName(\'' + local + '\')" class="form-control form-control-sm topBottomGap leftGap tsz14 transitionWidthS" id="componentSource' + local + '"></input>\
  </div><div class="under rightGap"><label for="predicateSelect" class="col-form-label col-form-label-sm tsz14 leftGap">Условие</label>\
  <select selectedValue="'+ currentRule.predicate + '" id="predicateSelect' + local + '" class="form-select tsz14 leftGap topBottomGap transitionWidthM" onchange="updateRulePredicate(\'' + local + '\')"><option value="Equal">Равно</option><option value="NotEqual">Неравно</option><option value="MoreThan">Больше чем</option>\
<option value="LessThan">Меньше чем</option><option value="Clicked">Безусловный переход</option></select></div>\
<div class="under rightGap"><label for="valueForCompare" class="col-form-label col-form-label-sm tsz14 leftGap">Значение для сравнения</label>\
<input type="text" onchange="updateRuleVal(\''+ local + '\')" class="form-control form-control-sm leftGap topBottomGap tsz14 transitionWidthS" value="' + currentRule.value + '" id="valueForCompare' + local + '"></div>\
</details>';

  if (local != 0) {
    // debugger;
    for (var i = 0; i < currentScreenObject.transitions.length; i++)
      for (var j = 0; j < currentScreenObject.transitions[i].rules.length; j++) {
        var curRule = currentScreenObject.transitions[i].rules[j];
        var containerId = currentScreenObject.transitions[i].rules[j].containerId;
        //debugger;
        document.getElementById("componentSource" + j).value = curRule.componentName;
        document.getElementById("predicateSelect" + j).value = curRule.predicate;
        document.getElementById("valueForCompare" + j).value = curRule.value;

      }
  }
}

function setTransForChoose(trans) {
  transForChoose = trans;
}

function setRuleForChoose(ruleC) {
  ruleForChoose = ruleC;
}

function loadCurrentRule(trans) {
  currentRule = currentScreenObject.transitions[trans].rules[ruleForChoose];
}

function saveRuleConditions(iterator) {
  currentRule = rulesPseudo[iterator];
}

function updateRuleDescr() {
  currentRule.description = '';//document.getElementById("componentSource"+local).value;
}

function updateRuleName(iterator) {
  currentRule.componentName = document.getElementById("componentSource" + iterator).value;
}

function updateRulePredicate(iterator) {

  //не сохраняется самое первое значение для предиката.
  currentRule.predicate = document.getElementById("predicateSelect" + iterator).value;
}


function updateRuleVal(iterator) {
  currentRule.value = document.getElementById("valueForCompare" + iterator).value;
}

function updateTransitionScreen(scr) {
  transition.nextScreen = scr;
}

function showAllTransitions() {
  //в скрине текущем идём в транзишены, и идём по ним циклом.внутри транзишена по массиву двигаем ещё одним циклом. один транзишен - один вызов addExistedTransition с присвоением ему айдишника
  //а вот всю внутрянку походов по рулам надо, вероятно, делать внутри addExistedTransition - то есть тот метод тож переписать половчее
  var localI = 0;
  var transitionsInScr = currentScreenObject.transitions;
  var transLen = currentScreenObject.transitions.length;
  let transitionArea = document.getElementById("transitions");
  //debugger;
  for (var i = 0; i < transLen; i++) {
    transitionArea.innerHTML += '<div class="under dottedBorder" onclick="loadCurrentRule(\'' + i + '\')" style="position:relative;width:100%">\
    <div class="under rightGap dottedBorder" style="width:100%;"><div class="form-group" style="text-align:left;width:100%;">\
    <label class="col-form-label col-form-label-sm tsz14 leftGap" for="screenDestination">Экран назначения</label>\
    <input type="text" class="form-control form-control-sm topBottomGap leftGap tsz14 transitionWidthS transitionHeight" onchange="updateTransitionScreen(this.value)"\
     id="screenDestination'+ i + '" value="' + transitionsInScr[i].nextScreen + '">\
    <div id="rulesContainer'+ i + '" class="under dottedBorder" style="width:100%;"></div></div>\
    <div class="under rightGap" style="height:100%;"><button style="height:35px;width:148px;" onclick="addNewRule(this.value)" value="'+ i + '"\
    class="tsz14 btn btn-info topBottomGap leftGap">Добавить правило</button></div></div></div>';

    var ruleLen = currentScreenObject.transitions[i].rules.length;
    for (var j = 0; j < ruleLen; j++) {
      var curRule = currentScreenObject.transitions[i].rules[j];
      document.getElementById("rulesContainer" + i).innerHTML += '<details onclick="setRuleForChoose(\'' + localI + '\')" style="height:80%;width:100%;text-align:left;" id="' + localI + '"><summary style="width:100%;">Правило\
      <label class="col-form-label col-form-label-sm tsz14 leftGap" alt="Удалить правило" onclick="deleteRule(\''+ i + '\',\'' + j + '\')">X</label></summary><div class="under rightGap">\
      <label class="col-form-label col-form-label-sm tsz14 leftGap" for="componentSource">Компонент</label>\
      <input type="text" value="'+ curRule.componentName + '" onchange="updateRuleName(\'' + localI + '\')" class="form-control form-control-sm topBottomGap leftGap tsz14 transitionWidthS" id="componentSource' + localI + '"></input>\
      </div><div class="under rightGap"><label for="predicateSelect" class="col-form-label col-form-label-sm tsz14 leftGap">Условие</label>\
      <select id="predicateSelect' + localI + '" class="form-select tsz14 leftGap topBottomGap transitionWidthM" onchange="updateRulePredicate(\'' + localI + '\')"><option value="Equal">Равно</option><option value="NotEqual">Неравно</option><option value="MoreThan">Больше чем</option>\
    <option value="LessThan">Меньше чем</option><option value="Clicked">Безусловный переход</option></select></div>\
    <div class="under rightGap"><label for="valueForCompare" class="col-form-label col-form-label-sm tsz14 leftGap">Значение для сравнения</label>\
    <input type="text" onchange="updateRuleVal(\''+ localI + '\')" class="form-control form-control-sm leftGap topBottomGap tsz14 transitionWidthS" value="' + curRule.value + '" id="valueForCompare' + localI + '"></div>\
    </details>';
      localI++;

    }

  }


  for (var i = 0; i < currentScreenObject.transitions.length; i++)
    for (var j = 0; j < currentScreenObject.transitions[i].rules.length; j++) {
      var curRule = currentScreenObject.transitions[i].rules[j];
      var containerId = currentScreenObject.transitions[i].rules[j].containerId;
      // debugger;
      document.getElementById("componentSource" + j).value = curRule.componentName;
      document.getElementById("predicateSelect" + j).value = curRule.predicate;
      document.getElementById("valueForCompare" + j).value = curRule.value;
    }

}


