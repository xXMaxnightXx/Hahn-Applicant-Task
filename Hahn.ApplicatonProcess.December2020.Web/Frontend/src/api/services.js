var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { HttpClient } from "aurelia-http-client";
import { inject } from "aurelia-framework";
import api from '../config/api';
var ApplicantService = (function () {
    function ApplicantService(http) {
        this.applicants = [];
        http.configure(function (x) { return x.withBaseUrl(api.dev + '/applicant/'); });
        this.http = http;
    }
    ApplicantService.prototype.getApplicants = function () {
        var _this = this;
        var promise = new Promise(function (resolve, reject) {
            _this.http
                .get('')
                .then(function (data) {
                _this.applicants = JSON.parse(data.response);
                resolve(_this.applicants);
            }).catch(function (err) { return reject(err); });
        });
        return promise;
    };
    ApplicantService.prototype.createApplcant = function (applicant) {
        var _this = this;
        var promise = new Promise(function (resolve, reject) {
            _this.http
                .post('', applicant)
                .then(function (data) {
                var newApplicant = JSON.parse(data.response);
                resolve(newApplicant);
            }).catch(function (err) { return reject(err); });
        });
        return promise;
    };
    ApplicantService.prototype.getApplicant = function (id) {
        var _this = this;
        var promise = new Promise(function (resolve, reject) {
            _this.http
                .get(id)
                .then(function (response) {
                var applicant = JSON.parse(response.response);
                resolve(applicant);
            }).catch(function (err) { return reject(err); });
        });
        return promise;
    };
    ApplicantService.prototype.deleteApplicant = function (id) {
        var _this = this;
        var promise = new Promise(function (resolve, reject) {
            _this.http
                .delete(api.dev + '/applicant/' + id)
                .then(function (response) {
            })
                .catch(function (err) { return reject(err); });
        });
        return promise;
    };
    ApplicantService.prototype.updateApplicant = function (id, applicant) {
        var _this = this;
        var promise = new Promise(function (resolve, reject) {
            _this.http
                .put(api.dev + '/applicant/' + id, applicant)
                .then(function (response) {
                var applicant = JSON.parse(response.response);
                resolve(applicant);
            }).catch(function (err) {
                return reject(err);
            });
        });
        return promise;
    };
    ApplicantService = __decorate([
        inject(HttpClient),
        __metadata("design:paramtypes", [HttpClient])
    ], ApplicantService);
    return ApplicantService;
}());
export { ApplicantService };
//# sourceMappingURL=services.js.map