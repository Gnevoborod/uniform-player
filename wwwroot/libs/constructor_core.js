//place of global variables definitions
let screenList = new Array();


let currentScreenObject = null;
let currentComponent = null;

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

function makeNewObject(componentType) {
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
    return true;
  }
  for (let i = 0; i < screenList.length; i++) {
    if (screenList[i].name.localeCompare(currentScreenObject.name) != 0) {
      screenList.push(currentScreenObject);
      return true;
    }
  }
}

function updateName(newValue) {
  currentScreenObject.name = newValue;
}

function showNewScreen(element) {
  let screenObject = makeNewScreen();
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
              <h5 class="card-title translucent">\
                Кнопка появится автоматически, если не будет кнопок-компонент</h5>\
            </div>'+ getNewScreenButton() + '</div>';
  document.getElementById("prototype").innerHTML = body;
  showSaveButton();
}



function showScreenSettings() {
  return '<div class="form-group  topBottomGap leftGap rightGap"><label for="screenNameConstructor">Идентификатор\
        экрана</label><input class="form-control topBottomGap" placeholder="Например: first_screen" type="text"\
        value="'+ currentScreenObject.name + '" id="screenNameConstructor" onchange="updateName(this.value)"></div>';
}



function showComponent(typeOfObj) {
  return '<div class="form-group  topBottomGap leftGap rightGap"><label for="componentNameConstructor">Идентификатор компонента</label><input class="form-control topBottomGap" placeholder="Например: text_box_1" type="text" value="" id="componentNameConstructor"></div>';
}

function addToScreen(componentType) {
  if (currentScreenObject == null)
    return;
  makeNewObject(componentType);
  currentScreenObject.components.push(currentComponent);
  document.getElementById("screenBody").innerHTML += getComponentForConstructor(componentType);
}

function excludeFromScreen(identity) {
  //найти элемент и удалить из массива
  //циклом отобразить элементы из списка элементов
}