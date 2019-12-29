import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {delay} from 'rxjs/operators';
import { TagService } from '../shared/tag.service';

@Component({
  selector: 'app-tag',
  templateUrl: './tag.component.html',
  styleUrls: ['./tag.component.scss']
})
export class TagComponent implements OnInit {

  constructor(public tagService: TagService) { }

  ngOnInit() {
    this.tagService.fetchTags()
  }

}
