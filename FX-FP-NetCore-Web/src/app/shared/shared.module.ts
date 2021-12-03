import { NgModule } from '@angular/core';
import { SHARED_ZORRO_MODULES } from "./shared-zorro.module";



@NgModule({
  declarations: [],
  imports: [
    SHARED_ZORRO_MODULES
  ],
  exports: [
    SHARED_ZORRO_MODULES
  ]
})
export class SharedModule { }
