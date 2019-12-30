import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {delay} from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { TagsService } from '../Shared/Services/tags.service';
import { Tag } from '../Shared/Models/Tag';



@Component({
  selector: 'app-tag',
  templateUrl: './tag.component.html',
  styleUrls: ['./tag.component.scss']
})
export class TagComponent implements OnInit {

  tag: Tag = new Tag();
  tags: Tag[];
  tableMode: boolean = true;
  
  constructor(private service: TagsService) {  }

  ngOnInit() {
    this.loaudTags();
  }

  loaudTags(){
    this.service.getTags().subscribe((date: Tag[]) => this.tags = date);
  }
 
  save(){
    if(this.tag.id == null){
      this.service.createTag(this.tag).subscribe((date: Tag) => this.tags.push(date));
    }
    else{
      this.service.updateTag(this.tag).subscribe(date => this.loaudTags())
    }
    this.cancel();
  }
  editTag(tag: Tag){
    this.tag = tag;
  }
  cancel(){
    this.tag = new Tag();
    this.tableMode = true;
  }
  delete(tag: Tag){
    this.service.deleteTag(tag.id).subscribe(date => this.loaudTags());
  }
  add(){
    this.cancel();
    this.tableMode = false;
  }
}
