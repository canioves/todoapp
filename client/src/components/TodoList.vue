<template>
  <div class="card">
    <div class="card-content">
      <h2 class="title is-4 has-text-centered">Список задач</h2>
      <ul>
        <li
          v-for="task in tasks"
          :key="task.id"
          class="box task-item"
          :class="{ 'task-item-checked': task.isCompleted }"
        >
          <div class="field is-grouped is-grouped-multiline">
            <div class="control">
              <label class="checkbox is-large">
                <input
                  type="checkbox"
                  :checked="task.isCompleted"
                  @change="toggleCompletion(task)"
                  class="custom-checkbox"
                />
              </label>
            </div>
            <div class="control is-expanded">
              <span
                class="is-size-5"
                :class="{ 'has-text-grey': task.isCompleted, 'is-italic': task.isCompleted }"
              >
                {{ task.description }}
              </span>
            </div>
            <div class="control">
              <button @click="deleteTask(task.id)" class="delete is-large"></button>
            </div>
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import { defineComponent, inject } from 'vue'

export default defineComponent({
  props: {
    tasks: {
      type: Array,
      required: false,
      default: () => [],
    },
  },
  emits: ['task-deleted'],
  setup(props, { emit }) {
    const $http = inject('$http')
    const toggleCompletion = async (task) => {
      task.isCompleted = !task.isCompleted
      await $http.put(`/tasks/complete/${task.id}`, task)
    }

    const deleteTask = async (id) => {
      await $http.delete(`/tasks/${id}`)
      emit('task-deleted', id)
    }

    return {
      toggleCompletion,
      deleteTask,
    }
  },
})
</script>

<style scoped>
.task-item {
  transition: background-color 0.2s ease;
}

.task-item:hover,
.task-item-checked {
  background-color: #1d1d1d; /* Мягкий серый цвет */
}

.custom-checkbox {
  width: 1.5rem; /* Увеличиваем размер чекбокса */
  height: 1.5rem; /* Увеличиваем размер чекбокса */
  margin-right: 1rem; /* Добавляем отступ справа */
}

.is-size-5 {
  font-size: 1.25rem; /* Увеличиваем размер шрифта */
}


</style>
