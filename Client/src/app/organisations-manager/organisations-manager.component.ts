import { Component, OnInit } from '@angular/core';
import { Organisation } from '../Shared/Models/Organisation';
import { OrganisationsManagerService } from '../Shared/Services/organisations-manager.service';

@Component({
  selector: 'app-organisations-manager',
  templateUrl: './organisations-manager.component.html',
  styleUrls: ['./organisations-manager.component.css']
})
export class OrganisationsManagerComponent implements OnInit {

  org: Organisation = new Organisation();
  orgs: Organisation[];
  tableMode: boolean = true;
  
  constructor(private service: OrganisationsManagerService) { }


  ngOnInit() {
    this.loaudOrgs();
  }


  loaudOrgs(){
    this.service.getOrgs().subscribe((date: Organisation[]) => this.orgs = date);

  }

  save(){
    if(this.org.id == null){
       return
    }
    else{
      this.service.updateOrg(this.org).subscribe(date => this.loaudOrgs())
    }
    this.cancel();
  }

  editTag(item: Organisation){
    this.org = item;
  }

  cancel(){
    this.org = new Organisation();
    this.tableMode = true;
  }

  delete(item: number){
    this.service.deleteOrg(item).subscribe(date => this.loaudOrgs());
  }
}
