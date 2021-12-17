import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-vk-callback',
  templateUrl: './vk-callback.component.html',
  styleUrls: ['./vk-callback.component.scss']
})
export class VkCallbackComponent implements OnInit {

  constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(params => {
      const code = params.get('code');

      this.http.post('users', {code})
        .subscribe(_ => this.router.navigateByUrl('/games'));
    });
  }
}
