import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {tap} from 'rxjs/operators';

export interface Tag {
    id: number
    title: string
  } 

@Injectable({providedIn: 'root'})
export class TagService{
    public tags: Tag[] = []

    constructor(private http: HttpClient){

    }

    fetchTags(): Observable<Tag[]>{
       return this.http.get<Tag[]>('https://localhost:44380/api/tag')
       .pipe(tap(tags => this.tags = tags))
    }


    addTodo(tag: Tag){
        this.tags.push(tag)
    }
}