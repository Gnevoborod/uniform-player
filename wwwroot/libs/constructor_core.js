//place of global variables definitions
let screenList = new Array();

let currentScreenObject = null;
let currentComponent = null;
let componentNames = new Array();

let firstScreen = '';
let firstcScreenChbChecked = false;

function makeNewScreen() {
  currentScreenObject = new Object();
  currentScreenObject.name = '';
  currentScreenObject.type = 'Common';
  currentScreenObject.title = '';
  currentScreenObject.components = new Array();
  currentScreenObject.pseudoName = '';
  currentScreenObject.description = '';

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

function getScreenData(screenName) {
  screenList.forEach(element => {
    if (element.name == screenName)
      return element;
  });

}

function saveScreenToList() {
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
  if (typeOfObj == 'TextArea') {
    val = '<label class="col-form-label col-form-label-sm " for="cmpValueConstructor">Значение\
    компонента по-умолчанию</label><textarea style="width:96%; margin-left:2%"\
     value="' + currentComponent.value + '" class="uniform-player-component form-control" placeholder="Например описание какого-либо процесса или требований" rows="5">' + currentComponent.value + '</textarea>';

    '<input class="form-control form-control-sm" placeholder="Например: 2023" type="text"\
    value="'+ currentComponent.value + '" id="cmpValueConstructor" onchange="updateCmpValue(this.value)"></input>';
  }
  else {
    val = '<label class="col-form-label col-form-label-sm " for="cmpValueConstructor">Значение\
    компонента по-умолчанию</label><input class="form-control form-control-sm" placeholder="Например: 2023" type="text"\
    value="'+ currentComponent.value + '" id="cmpValueConstructor" onchange="updateCmpValue(this.value)"></input>';
  }
  return '<div class="form-group topBottomGap leftGap rightGap"><label class="col-form-label col-form-label-sm" for="componentNameConstructor">Идентификатор компонента</label>\
  <input class="form-control form-control-sm" placeholder="Например: text_box_1" type="text" value="'+ currentComponent.name + '" id="componentNameConstructor" onchange="updateCmpName(this.value)">\
  <label class="col-form-label col-form-label-sm " for="cmpDescriptionConstructor">Описание\
        компонента</label><input class="form-control form-control-sm" placeholder="Например: Компонент выбора даты" type="text"\
        value="'+ currentComponent.description + '" id="cmpDescriptionConstructor" onchange="updateCmpDescription(this.value)"></input>'
    + val +
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

function updateCmpValue(val) {
  currentComponent.value = val;
}

function updateCmpProperties(props) {
  currentComponent.properties = props;
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
  var firstScreenV = { firstScreen: firstScreen };
  var full = { configuration: firstScreenV, screens: screenList, transitions: null };
  var request = JSON.stringify(full);
  console.log(request);
}