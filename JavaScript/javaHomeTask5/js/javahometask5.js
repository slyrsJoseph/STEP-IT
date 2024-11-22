////////////////////////TASK 1
// class Marker {
//   constructor(markerColor, ink) {
//     this.markerColor = markerColor;
//     this.ink = ink;
//   }

//   print(text) {
//     let output = "";
//     for (let i = 0; i < text.length; i++) {
//       if (this.ink <= 0) {
//         console.log("Ink is out");
//         break;
//       }

//       const char = text[i];
//       if (char !== " ") {
//         this.ink -= 0.5;
//       }
//       output += char;
//     }
//     console.log(`Text: ${output}`);
//     console.log(`Level of ink remained: ${this.ink.toFixed(1)}%`);
//   }
// }
// class RefillInk extends Marker {
//   refill() {
//     this.ink = 100;
//     console.log("Marker is refilled with ink");
//   }
// }
// const marker = new Marker("red", 20);
// marker.print("hihihihihihihihihihihihihihihi");
// marker.print("lets print again...");

// const refillMarker = new RefillInk("black", 10);
// refillMarker.print("refilling marker");
// refillMarker.refill();
// refillMarker.print("marker is ready to write");

////////////////////////////////TASK 2

// class ExtendedDate extends Date {
//   getDateText() {
//     const day = this.getDate();
//     const month = this.getMonth() + 1;
//     return `${day}.${month}`;
//   }

//   isFutureDate() {
//     const currentTime = new Date();
//     return this >= currentTime;
//   }

//   isLeap() {
//     if (this.year % 4 === 0) {
//       return true;
//     }
//     return false;
//   }

//   nextDate() {
//     const nextDate = new Date(this);
//     nextDate.setDate(this.getDate() + 1);
//     return nextDate;
//   }
// }
// const extendedDate = new ExtendedDate(2024, 10, 22);
// console.log(extendedDate.getDateText());
// console.log(extendedDate.isFutureDate());
// console.log(extendedDate.isLeap());
// console.log(extendedDate.nextDate().toISOString().slice(0, 10));

//////////////////////////TASk 3
// class Employee {
//   constructor(name, position, salary) {
//     this.name = name;
//     this.position = position;
//     this.salary = salary;
//   }
// }
// class EmpTable {
//   constructor(employees) {
//     this.employees = employees;
//   }
//   getHtml() {
//     let html = `
//       <table border="1" cellspacing="0" cellpadding="5">
//         <thead>
//           <tr>
//             <th>name</th>
//             <th>position</th>
//             <th>salary</th>
//           </tr>
//         </thead>
//         <tbody>
//     `;

//     this.employees.forEach((employee) => {
//       html += `
//         <tr>
//           <td>${employee.name}</td>
//           <td>${employee.position}</td>
//           <td>${employee.salary}</td>
//         </tr>
//       `;
//     });

//     html += `
//         </tbody>
//       </table>
//     `;
//     return html;
//   }
// }
// const employees = [
//   new Employee("Ali", "Engineer", 5000),
//   new Employee("Mamed", "Seller", 700),
//   new Employee("Vugar", "Security", 1200),
//   new Employee("Ehmed", "Manager", 1500),
// ];
// const empTable = new EmpTable(employees);
// document.write(empTable.getHtml());

//////////////////TASK 4
class EmpTable {
  getHtml() {
    return `
          <table>
              <tr>
                  <th>Name</th>
                  <th>Position</th>
              </tr>
              <tr>
                  <td>Ахмед</td>
                  <td>десантник</td>
              </tr>
          </table>
      `;
  }
}

class StyledEmpTable extends EmpTable {
  getStyles() {
    return `
          <style>
              table { 
                  border-collapse: collapse; 
                  width: 50%; 
              }
              th, td { 
                  padding: 8px 12px; 
                  border: 1px solid #ddd; 
                  text-align: left; 
              }
              th { 
                  background-color: white; 
              }
          </style>
      `;
  }

  getHtml() {
    return this.getStyles() + super.getHtml();
  }
}

const styledTable = new StyledEmpTable();
document.body.innerHTML = styledTable.getHtml();
