<template>
  <div class="card">
    <div class="card-content">
      <h2 class="title is-4 has-text-centered">Добавить задачу</h2>
      <form @submit.prevent="addTask">
        <div class="field">
          <div class="control has-icons-left">
            <input
              v-model="newTask.description"
              class="input"
              placeholder="Название задачи"
              required
            />
            <span class="icon is-small is-left">
              <i class="fas fa-tasks"></i>
            </span>
          </div>
        </div>
        <div class="field">
          <div class="control">
            <button type="submit" class="button is-primary is-fullwidth">
              <span class="icon">
                <i class="fas fa-plus"></i>
              </span>
              <span>Добавить</span>
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { defineComponent, ref, inject } from 'vue'

export default defineComponent({
  emits: ['task-added'],
  setup(_, { emit }) {
    const $http = inject('$http')
    const newTask = ref({
      description: '',
      isCompleted: false,
    })

    const addTask = async () => {
      const response = await $http.post('/tasks', newTask.value)
      emit('task-added', response.data)
      newTask.value.description = ''
    }

    return {
      newTask,
      addTask,
    }
  },
})
</script>

<style scoped>
.card {
  margin-bottom: 1.5rem;
}
</style>