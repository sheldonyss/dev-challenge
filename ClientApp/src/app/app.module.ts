import { NodeServiceProxy } from './../service-proxies/service-proxies';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { API_BASE_URL } from '../service-proxies/service-proxies';
import { AppConsts } from '../app-constant';
import { HttpClientModule } from '@angular/common/http';

// import { FlexLayoutModule } from '@angular/flex-layout';

export function getRemoteServiceBaseUrl(): string {
  return AppConsts.remoteServiceBaseUrl;
}

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule
    // , FlexLayoutModule
  ],
  providers: [
    { provide: API_BASE_URL, useFactory: getRemoteServiceBaseUrl },
    NodeServiceProxy
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
