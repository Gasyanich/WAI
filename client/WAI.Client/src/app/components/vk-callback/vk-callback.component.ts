import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-vk-callback',
  templateUrl: './vk-callback.component.html',
  styleUrls: ['./vk-callback.component.scss']
})
export class VkCallbackComponent implements OnInit {

  constructor(private route: ActivatedRoute, private auth: AuthService) {
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(params => {
      const code = params.get('code');

      if (code)
        this.auth.loginVk(code);
    });
  }
}
