import { Component, ViewChild, Inject } from '@angular/core';
import { MatDialog, MatTable } from '@angular/material';
import { DialogBoxComponent } from './dialog-box/dialog-box.component';
import { HttpClient } from '@angular/common/http';

export interface UsersData {
  name: string;
  id: number;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})

export class AppComponent {
  displayedColumns: string[] = ['Ime', 'Prezime', 'Broj', 'Adresa', 'action'];
  dataSource: any;
  public tablica: Tablica;
  @ViewChild(MatTable, { static: true }) table: MatTable<any>;

  constructor(public dialog: MatDialog, public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    http.get<Tablica[]>(baseUrl + 'api/Tablicas').subscribe(result => {
      this.dataSource = result;
    }, error => console.error(error));
  }

  openDialog(action, obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '300px',
      data: obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event == 'Add') {
        this.addRowData(result.data);
      } else if (result.event == 'Update') {
        this.updateRowData(result.data);
      } else if (result.event == 'Delete') {
        this.deleteRowData(result.data);
      }
    });
  }

  addRowData(newRow) {
    this.dataSource.push({
      Ime: newRow.Ime,
      Prezime: newRow.Prezime,
      Broj: newRow.Broj,
      Adresa: newRow.Adresa
    });

    this.tablica = {
      id: newRow.Id,
      Ime: newRow.Ime,
      Prezime: newRow.Prezime,
      Broj: newRow.Broj,
      Adresa: newRow.Adresa
    };
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(this.tablica);
    this.http.post<Tablica>(this.baseUrl + 'api/Tablicas', body, { 'headers': headers }).subscribe(data => {
    })

    this.table.renderRows();

  }
  updateRowData(newRow) {
    this.dataSource = this.dataSource.filter((value, key) => {
      if (value.id == newRow.id) {
        value.Ime = newRow.Ime;
        value.Prezime= newRow.Prezime;
        value.Broj = newRow.Broj;
        value.Adresa = newRow.Adresa;
      }

      this.tablica = {
        id: newRow.id,
        Ime: newRow.Ime,
        Prezime: newRow.Prezime,
        Broj: newRow.Broj,
        Adresa: newRow.Adresa
      };
      const headers = { 'content-type': 'application/json' }
      const body = JSON.stringify(this.tablica);
      this.http.put<Tablica[]>(this.baseUrl + 'api/Tablicas/' + newRow.id, body, { 'headers': headers }).subscribe(data => {
      })
      return true;
    });
  }
  deleteRowData(newRow) {
    this.dataSource = this.dataSource.filter((value, key) => {
      this.http.delete<any>(this.baseUrl + 'api/Tablicas/' + newRow.id).subscribe(data => {
      })
      return value.id != newRow.id;
    });
  }
}


interface Tablica {
  id: string;
  Ime: number;
  Prezime: number;
  Broj: string;
  Adresa: string;
}
