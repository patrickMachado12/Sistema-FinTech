<template>
  <v-card>
    <v-card-title>
      <span class="text-h5">{{ formTitulo }}</span>
    </v-card-title>
    <v-card-text>
      <v-container>
        <v-row>
          <v-col cols="12">
            <v-text-field
              v-model="editedItem.nome"
              label="Nome"
            ></v-text-field>
          </v-col>
            
          <v-col cols="12">
            <v-text-field
              v-model="editedItem.telefone"
              label="Telefone"
            ></v-text-field>
          </v-col>

          <!-- <v-col cols="12">
            <v-text-field
              v-model="editedItem.email"
              label="Email"
            ></v-text-field>
          </v-col> -->
          
          
        </v-row>
      </v-container>
      <small>*Todos os campos são obrigatórios</small>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn
        color="blue darken-1"
        text
        @click="dialog = false"
      >
        Fechar
      </v-btn>
      <v-btn
        color="blue darken-1"
        text
        @click="gravar"
      >
        Gravar
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
  export default {
    name: 'CardPessoa',
    data: () => ({
      dialog: false,
    }),
  }
</script>

<style scoped>

</style>