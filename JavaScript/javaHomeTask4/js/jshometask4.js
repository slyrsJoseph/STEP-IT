///////////////////////////////TASK 1
// class Circle {
//   #radius;
//   constructor(radius) {
//     this.#radius = radius;
//   }

//   get radius() {
//     return this.#radius;
//   }

//   set radius(value) {
//     this.#radius = value;
//   }

//   get diameter() {
//     return this.#radius * 2;
//   }

//   circleArea() {
//     return 3.14 * this.#radius ** 2;
//   }

//   circleLength() {
//     return 2 * 3.14 * this.#radius;
//   }
// }
// const circle = new Circle(5);

// console.log(`Circle radius: ${circle.radius}`);
// console.log(`Circle diameter: ${circle.diameter}`);
// console.log(`Circle area: ${circle.circleArea()}`);
// console.log(`Circle length: ${circle.circleLength()}`);

// circle.radius = 10;
// console.log(`\nNew Circle radius ${circle.radius}`);
// console.log(`Circle diameter: ${circle.diameter}`);
// console.log(`Circle area: ${circle.circleArea()}`);
// console.log(`Circle length: ${circle.circleLength()}`);

/////////////////////////////////TASK 2

// class HtmlElement {
//   constructor(tagName, isSelfClosing = false, textContent = "") {
//     this.tagName = tagName;
//     this.isSelfClosing = isSelfClosing;
//     this.textContent = textContent;
//     this.attributes = {};
//     this.styles = {};
//     this.children = [];
//   }

//   setAttribute(name, value) {
//     this.attributes[name] = value;
//   }

//   setStyle(property, value) {
//     this.styles[property] = value;
//   }

//   appendChild(child) {
//     this.children.push(child);
//   }

//   prependChild(child) {
//     this.children.unshift(child);
//   }

//   getHtml() {
//     let attributesString = "";
//     const attributesEntries = Object.entries(this.attributes);
//     if (attributesEntries.length > 0) {
//       attributesString = attributesEntries
//         .map(([key, value]) => `${key}="${value}"`)
//         .join(" ");
//     }

//     let stylesString = "";
//     const stylesEntries = Object.entries(this.styles);
//     if (stylesEntries.length > 0) {
//       stylesString = stylesEntries
//         .map(([key, value]) => `${key}: ${value};`)
//         .join(" ");
//     }

//     let openingTag = `<${this.tagName}`;
//     if (attributesString) {
//       openingTag += " " + attributesString;
//     }
//     if (stylesString) {
//       openingTag += ' style="' + stylesString + '"';
//     }
//     if (this.isSelfClosing) {
//       openingTag += " /";
//     }
//     openingTag += ">";

//     let content = "";
//     if (!this.isSelfClosing) {
//       const childrenHtml = this.children
//         .map((child) => child.getHtml())
//         .join("");
//       content = this.textContent + childrenHtml;
//     }

//     let closingTag = "";
//     if (!this.isSelfClosing) {
//       closingTag = `</${this.tagName}>`;
//     }

//     return openingTag + content + closingTag;
//   }
// }

// const div = new HtmlElement("div");
// div.setAttribute("class", "container");
// div.setStyle("width", "100px");
// div.setStyle("height", "100px");
// div.setStyle("background-color", "black");

// const span = new HtmlElement("span", false, "Hello, World!");
// span.setStyle("color", "red");
// div.appendChild(span);

// document.write(div.getHtml());

//////////////////////////////////////////////TASK 3

// class CssClass {
//   constructor(name) {
//     this.name = name;
//     this.styles = {};
//   }
//   setStyle(property, value) {
//     this.styles[property] = value;
//   }
//   deleteStyle(property) {
//     delete this.styles[property];
//   }
//   getCss() {
//     let cssString = `${this.name} {`;
//     for (let property in this.styles) {
//       cssString += `\n ${property}: ${this.styles[property]};`;
//     }
//     cssString += "\n}";
//     return cssString;
//   }
// }
// const obj = new CssClass("test-object");
// obj.setStyle("color", "black");
// obj.setStyle("background-color", "red");
// console.log(obj.getCss());
// obj.deleteStyle("background-color");
// console.log(obj.getCss());

///////////////////////////////////////////////////TASK 4

class HtmlElement {
  constructor(tagName, isSelfClosing = false, textContent = "") {
    this.tagName = tagName;
    this.isSelfClosing = isSelfClosing;
    this.textContent = textContent;
    this.attributes = {};
    this.styles = {};
    this.children = [];
  }

  setAttribute(name, value) {
    this.attributes[name] = value;
  }

  setStyle(property, value) {
    this.styles[property] = value;
  }

  appendChild(child) {
    this.children.push(child);
  }

  prependChild(child) {
    this.children.unshift(child);
  }

  getHtml() {
    let attributesString = "";
    const attributesEntries = Object.entries(this.attributes);
    if (attributesEntries.length > 0) {
      attributesString = attributesEntries
        .map(([key, value]) => `${key}="${value}"`)
        .join(" ");
    }

    let stylesString = "";
    const stylesEntries = Object.entries(this.styles);
    if (stylesEntries.length > 0) {
      stylesString = stylesEntries
        .map(([key, value]) => `${key}: ${value};`)
        .join(" ");
    }

    let openingTag = `<${this.tagName}`;
    if (attributesString) {
      openingTag += " " + attributesString;
    }
    if (stylesString) {
      openingTag += ' style="' + stylesString + '"';
    }
    if (this.isSelfClosing) {
      openingTag += " /";
    }
    openingTag += ">";

    let content = "";
    if (!this.isSelfClosing) {
      const childrenHtml = this.children
        .map((child) => child.getHtml())
        .join("");
      content = this.textContent + childrenHtml;
    }

    let closingTag = "";
    if (!this.isSelfClosing) {
      closingTag = `</${this.tagName}>`;
    }

    return openingTag + content + closingTag;
  }
}

class CssClass {
  constructor(name) {
    this.name = name;
    this.styles = {};
  }
  setStyle(property, value) {
    this.styles[property] = value;
  }
  deleteStyle(property) {
    delete this.styles[property];
  }
  getCss() {
    let cssString = `${this.name} {`;
    for (let property in this.styles) {
      cssString += `\n ${property}: ${this.styles[property]};`;
    }
    cssString += "\n}";
    return cssString;
  }
}

class HtmlBlock {
  constructor() {
    this.cssClasses = [];
    this.rootElement = null;
  }
  addClass(cssClass) {
    this.cssClasses.push(cssClass);
  }
  setElement(element) {
    this.rootElement = element;
  }
  getCode() {
    let cssCode = "<style>";
    for (let cssClass of this.cssClasses) {
      cssCode += `\n${cssClass.getCss()}`;
    }
    cssCode += "\n</style>";
    let htmlCode = this.rootElement.getHtml();
    return cssCode + htmlCode;
  }
}
let css1 = new CssClass(".wrap");
css1.setStyle("display", "flex");

let css2 = new CssClass(".block");
css2.setStyle("width", "300px");
css2.setStyle("margin", "10px");

let css3 = new CssClass(".img");
css3.setStyle("width", "100%");

let css4 = new CssClass(".text");
css4.setStyle("text-align", "justify");

let div = new HtmlElement("div");
div.setAttribute("id", "wrapper");
div.setAttribute("class", "wrap");

let div2 = new HtmlElement("div");
div2.setAttribute("class", "block");

let h = new HtmlElement("h3", false, "What is Lorem Ipsum?");

let img = new HtmlElement("img");
img.setAttribute("class", "img");
img.setAttribute("src", "lipsum.jpg");
img.setAttribute("alt", "Lorem Ipsum");

let p = new HtmlElement("p", false, "Some text about lorem ipsum");

let a = new HtmlElement("a", false, "Moree");
a.setAttribute("href", "https://www.lipsum.com");
a.setAttribute("target", "_blank");

p.setAttribute("class", "text");
let block = new HtmlBlock();
block.addClass(css1);
block.addClass(css2);
block.addClass(css3);
block.addClass(css4);
block.setElement(div);
div.appendChild(div2);
div2.appendChild(h);
div2.appendChild(img);
div2.appendChild(p);
div2.appendChild(a);
div.appendChild(div2);
document.write(block.getCode());
