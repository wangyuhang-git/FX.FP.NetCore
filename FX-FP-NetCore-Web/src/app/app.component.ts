import { ChangeDetectionStrategy,Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template:`
  <router-outlet></router-outlet>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush
  //templateUrl: './app.component.html',
  //styleUrls: ['./app.component.less']
})
export class AppComponent {
  title = 'FX-FP-NetCore-Web';
}

