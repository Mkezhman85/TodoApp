<template>
  <q-dialog persistent v-model="userEditorOpened">
    <q-card style="max-width: 700px">
      <q-bar class="bg-primary">
        <div class="text-h6 text-white">Просмотр пользователя</div>
        <q-space />
        <q-btn dense flat icon="close" @click="hideUserEditor">
          <q-tooltip>Close</q-tooltip>
        </q-btn>
      </q-bar>

      <q-card-section>
        <div class="row">
          <div class="col">
            <q-input
              label="login"
              outlined
              clearable
              dense
              class="q-pa-sm"
              v-model="user.login"
            />
          </div>
          <div class="col">
            <q-input
              label="begin_date"
              outlined
              clearable
              dense
              class="q-pa-sm"
              v-model="user.begin_date"
            />
            <q-input
              label="end_date"
              outlined
              clearable
              dense
              class="q-pa-sm"
              v-model="user.end_date"
            />
          </div>
        </div>
        <div class="row">
          <div class="col">
            <q-input
              label="is_blocked"
              outlined
              clearable
              dense
              class="q-pa-sm"
              v-model="user.is_blocked"
            />
            <q-input
              label="required_password_change"
              outlined
              clearable
              dense
              class="q-pa-sm"
              v-model="user.required_password_change"
            />
          </div>
          <div class="col">
            <q-input
              label="wrong_login_count"
              outlined
              clearable
              dense
              class="q-pa-sm"
              v-model="user.wrong_login_count"
              type="number"
            />
            <q-input
              label="last_login_date"
              outlined
              clearable
              dense
              class="q-pa-sm"
              v-model="user.last_login_date"
            />
          </div>
        </div>
      </q-card-section>

      <q-separator />

      <q-card-actions class="row no-wrap">
        <q-space />

        <q-btn color="positive" label="сохранить" @click="CreateNewUser(user)" />

        <q-btn class="q-ml-sm" flat color="red" label="сбросить изменения" />
        <q-btn
          class="q-ml-sm"
          flat
          color="red"
          label="тест"
        />
      </q-card-actions>

      <q-inner-loading>
        <q-spinner-gears size="48px" color="primary" />
      </q-inner-loading>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { Component, Prop, Vue, PropSync } from "vue-property-decorator";
import { TSelect, TInput, TCheckbox } from "src/types";
import axios from "axios";
import { type } from "os";


@Component
export default class UserEditor extends Vue {
  @PropSync("value", { type: Boolean }) userEditorOpened!: string;
  openUserEditor() {
    this.$emit("open-userEditorOpened", this.userEditorOpened);
  }
  hideUserEditor() {
    this.$emit("hide-userEditorOpened", this.userEditorOpened);
  }

  @Prop(Object) readonly user: object;

  // @Prop(Number) readonly id: number;
  // @Prop(String) readonly login: string;
  // @Prop(String) readonly begin_date: string;
  // @Prop(String) readonly end_date: string;
  // @Prop(Boolean) readonly is_blocked: boolean;
  // @Prop(Boolean) readonly required_password_change: boolean;
  // @Prop(Number) readonly wrong_login_count: number;
  // @Prop(String) readonly last_login_date: string;
  

  CreateNewUser(user: object) {
    // debugger;
    // user = this.user
    this.$emit("create-newUser", this.user);
    console.log(user);
  }

  // showNewUserConsole(user: object) {
  //   user = this.user;
  //   console.log(user);
  // }
}
</script>
